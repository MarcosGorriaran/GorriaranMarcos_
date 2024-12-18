using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField]
    public Group targetGroup;
    [SerializeField]
    public float detectionRange;
    [SerializeField]
    public bool seeTroughObstacles;
    private bool targetFound;
    public delegate void TargetFoundAction(Collider2D objectFound);
    public event TargetFoundAction onTargetedFound;
    public delegate void TargetLostAction();
    public event TargetLostAction onTargetLost;


    void FixedUpdate()
    {
        SearchTargets();
    }
    private void SearchTargets()
    {
        Collider2D[] elementsFound = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        elementsFound = elementsFound.Where(IsTargatable).ToArray();
        try
        {

            if (elementsFound.Count() > 0 && !targetFound)
            {
                Debug.Log(gameObject.name + " \"TargetFound\"");
                onTargetedFound.Invoke(SelectHighestThreat(elementsFound));
                targetFound = true;
            }
            else if (elementsFound.Count() <= 0 && targetFound)
            {
                Debug.Log(gameObject.name + " \"TargetLost\"");
                onTargetLost.Invoke();
                targetFound = false;
            }
        }
        catch (NullReferenceException)
        {

        }
    }
    private bool IsTargatable(Collider2D element)
    {
        bool result = element.TryGetComponent(out ITargetable targetable);
        if (result)
        {
            result = targetable.GroupMember == targetGroup;
            if(result && !seeTroughObstacles)
            {
                result = LineOfShightCheck(element);
            }
        }
        return result;
    }
    private bool LineOfShightCheck(Collider2D element)
    {
        Vector2 distanceVector = element.transform.position - transform.position;
        float distance = Vector2.Distance(element.transform.position, transform.position);
        Vector2 direction = distanceVector.normalized;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, distance);
        return hit.Where((obj => !obj.transform.TryGetComponent<ITargetable>(out _))).Count() == 0;
    }
    private Collider2D SelectHighestThreat(Collider2D firstElement, Collider2D secondElement)
    {
        return firstElement.GetComponent<ITargetable>().ThreatLevel > secondElement.GetComponent<ITargetable>().ThreatLevel ? firstElement : secondElement;
    }
    private Collider2D SelectHighestThreat(Collider2D[] elements)
    {
        return elements.Aggregate(SelectHighestThreat);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
    }
    private void LookForTarget()
    {
        Collider2D[] elementsFound = Physics2D.OverlapCircleAll(transform.position, detectionRange);
    }
}

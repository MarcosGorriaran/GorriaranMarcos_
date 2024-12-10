using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    public float detectionRange;
    [SerializeField]
    public bool seeTroughObstacles;
    public delegate void TargetFoundAction();
    public event TargetFoundAction onTargetedFound;
    public delegate void TargetLostAction();
    public event TargetLostAction onTargetLost;


    // Update is called once per frame
    void Update()
    {
        Collider2D[] elementsFound = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        elementsFound = elementsFound.Where((element) =>
        {
            bool result = element.gameObject == target;
            if (!seeTroughObstacles && result)
            {
                Vector3 direction = (element.transform.position - transform.position).normalized;
                RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction);
                result = rayHit.collider.gameObject == target;
            }
            return result;
        }).ToArray();
        if (elementsFound.Count() > 0)
        {
            onTargetedFound.Invoke();
        }
        else
        {
            onTargetLost.Invoke();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
    private void LookForTarget()
    {
        Collider2D[] elementsFound = Physics2D.OverlapCircleAll(transform.position, detectionRange);
    }
}

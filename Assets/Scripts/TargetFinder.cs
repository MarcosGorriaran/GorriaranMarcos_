using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    public float detectionRange;
    public delegate void TargetFoundAction();
    public event TargetFoundAction onTargetedFound;
    

    // Update is called once per frame
    void Update()
    {
        Collider2D[] elementsFound = Physics2D.OverlapCircleAll(transform.position, detectionRange);    
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

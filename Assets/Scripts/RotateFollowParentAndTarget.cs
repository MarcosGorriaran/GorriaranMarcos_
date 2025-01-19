using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollowParentAndTarget : MonoBehaviour
{
    public Transform target;
    public float parentPositionOffset;
    private void Update()
    {
        Vector2 dir = GetDir();
        transform.localPosition = dir * parentPositionOffset;
        transform.rotation = GetAngleRotation(dir);
    }
    private Vector2 GetDir()
    {
        return (target.position - transform.parent.position).normalized;
    }
    private Quaternion GetAngleRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        return Quaternion.Euler(0, 0, angle * 180f / Mathf.PI - 90f);
    }
}

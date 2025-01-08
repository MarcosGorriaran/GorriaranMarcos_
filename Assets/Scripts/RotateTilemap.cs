using UnityEngine;

public class RotateTilemap : MonoBehaviour
{
    [SerializeField]
    Transform[] keepRotationFromChildren;

    public void Rotate(float angle)
    {

        transform.eulerAngles += new Vector3(0, 0, angle);

        KeepChildrenRotation(angle);
    }
    private void KeepChildrenRotation(float angle)
    {
        foreach (Transform t in keepRotationFromChildren)
        {
            t.eulerAngles += new Vector3(0, 0, angle * -1);

        }
    }
}

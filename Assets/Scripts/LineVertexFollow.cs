using UnityEngine;

public class LineVertexFollow : MonoBehaviour
{
    [SerializeField]
    Transform[] vertextPoints;
    LineRenderer lineRenderer;
    [SerializeField]
    Vector3 lineOffset;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertextPoints.Length;
    }
    private void Update()
    {
        for(int i = 0; i < vertextPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, vertextPoints[i].position +lineOffset);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public enum SectionType
{
    XSection,
    TSectionENW,
    TSectionESW,
    TSectionNES,
    TSectionNWS,
    LineSectionEW,
    LineSectionNS,
    CornerSectionES,
    CornerSectionNE,
    CornerSectionSW,
    CornerSectionWN
}
public class ProceduralNode : MonoBehaviour
{
    public static List<ProceduralNode> nodeList = new List<ProceduralNode>();
    [SerializeField]
    public SectionType type;
    private void OnDestroy()
    {
        nodeList.Remove(this);
    }
    private void Awake()
    {
        nodeList.Add(this);
    }
}

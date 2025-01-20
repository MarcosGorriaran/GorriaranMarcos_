using NavMeshPlus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrecuduralNodeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] nodeMapOptions;
    [SerializeField]
    ProceduralMapItemsSO[] generationOptions;
    [SerializeField]
    NavMeshSurface navMeshSurface;

    private void Awake()
    {
        PickANodeMap();
    }
    void Start()
    {
        SetNodesUp();
    }
    void PickANodeMap()
    {
        Instantiate(nodeMapOptions[UnityEngine.Random.Range(0,nodeMapOptions.Length)]);
    }
    void SetNodesUp()
    {
        ProceduralMapItemsSO selectedOption = generationOptions[UnityEngine.Random.Range(0, generationOptions.Length)];
        Dictionary<SectionType, ProceduralNode[]> sectionList = new Dictionary<SectionType, ProceduralNode[]>();
        foreach (SectionType type in Enum.GetValues(typeof(SectionType)))
        {
            sectionList.Add(type, ProceduralNode.nodeList.Where(node => node.type == type).ToArray());
        }

        foreach (KeyValuePair<SectionType, ProceduralNode[]> nodeList in sectionList)
        {
            FillSection(nodeList.Value, selectedOption.sectionObjects.Where(obj => obj.sectionLabel == nodeList.Key).ToArray());
        }

        navMeshSurface.BuildNavMesh();
    }
    void FillSection<T>(ProceduralNode[] nodes, T[] fillOption) where T : MonoBehaviour
    {
        foreach(ProceduralNode node in nodes)
        {
            try
            {
                GameObject instantiatedElement = Instantiate(fillOption[UnityEngine.Random.Range(0, fillOption.Length)].gameObject);
                instantiatedElement.transform.position = node.transform.position;
            }
            catch (IndexOutOfRangeException)
            {

            }
            
        }
    }
}

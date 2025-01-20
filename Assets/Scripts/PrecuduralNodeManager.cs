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
    [SerializeField]
    Player playerPrefab;

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

        ProceduralNode startingPoint = sectionList.Where(dict => dict.Key == SectionType.PlayerStart).FirstOrDefault().Value.FirstOrDefault();
        FillSection(startingPoint, selectedOption.sectionObjects.Where(obj => obj.sectionLabel == SectionType.PlayerStart).ToArray());
        sectionList.Remove(SectionType.PlayerStart);
        
        foreach (KeyValuePair<SectionType, ProceduralNode[]> nodeList in sectionList)
        {
            FillSection(nodeList.Value, selectedOption.sectionObjects.Where(obj => obj.sectionLabel == nodeList.Key).ToArray());
        }

        navMeshSurface.BuildNavMesh();
    }
    void InstantiatePlayer(Vector2 location)
    {
        Player player = Player.instance == null ? Instantiate(playerPrefab) : Player.instance;
        player.transform.position = location;
    }
    void FillSection(ProceduralNode[] nodes, SectionTypeLabel[] fillOption)
    {
        foreach(ProceduralNode node in nodes)
        {
            FillSection(node, fillOption);
        }
    }
    void FillSection(ProceduralNode node, SectionTypeLabel[] fillOption)
    {
        try
        {
            SectionTypeLabel label = fillOption[UnityEngine.Random.Range(0, fillOption.Length)];
            GameObject instantiatedElement = Instantiate(label.gameObject);
            instantiatedElement.transform.position = node.transform.position;
            if(label.playerSpawnLocation != null)
            {
                InstantiatePlayer(instantiatedElement.GetComponent<SectionTypeLabel>().playerSpawnLocation.position);
            }
        }
        catch (IndexOutOfRangeException)
        {

        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "MapList", menuName = "ScriptableObjects/ProceduralMapItems", order = 1)]
public class ProceduralMapItemsSO : ScriptableObject
{
    public GameObject[] xSections;
    public GameObject[] tSections;
    public GameObject[] lineSections;
    public GameObject[] deadEndSections;
}

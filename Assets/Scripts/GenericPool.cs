using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    [SerializeField]
    GameObject entity;
    List<GameObject> pool = new List<GameObject>();

    public GameObject InstantiateObject(Vector3 position)
    {
        GameObject instantiatedEntity = Create();
        entity.transform.position = position;
        return instantiatedEntity;
    }
    private GameObject Create()
    {
        GameObject[] disabledElements = pool.Where(obj => obj.gameObject.activeSelf).ToArray();
        GameObject instancedElement;
        if (disabledElements.Length>0)
        {
            instancedElement = disabledElements[0];
            instancedElement.gameObject.SetActive(true);
        }
        else
        {
            instancedElement = Instantiate(entity);
            pool.Add(instancedElement);
        }
        return instancedElement;
    }
}

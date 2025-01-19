using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Key : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elementsToDestroy;
    [SerializeField]
    private GameObject[] elementsToActivate;
    [SerializeField]
    GameObject[] elementsToDeactivate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out _))
        {
            foreach (GameObject element in elementsToDestroy)
            {
                Destroy(element);
            }
            foreach(GameObject element in elementsToActivate)
            {
                element.SetActive(true);
            }
            foreach(GameObject element in elementsToDeactivate)
            {
                element.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}

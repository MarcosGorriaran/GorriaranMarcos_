using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDisabler : MonoBehaviour
{
    public void DisableObjectOn(IEnumerator conditionalTimer, GameObject toBeDisabled)
    {
        StartCoroutine(WaitForDisable(conditionalTimer,toBeDisabled));
    }
    private IEnumerator WaitForDisable(IEnumerator conditionalTimer, GameObject toBeDisabled)
    {
        yield return conditionalTimer;
        toBeDisabled.SetActive(false);
    }
}

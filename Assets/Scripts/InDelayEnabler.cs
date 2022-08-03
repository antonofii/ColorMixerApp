using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDelayEnabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private float delay;

    private IEnumerator Start()
    {
        foreach (GameObject obj in objectsToEnable)
            obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in objectsToEnable)
            obj.SetActive(true);
    }
}

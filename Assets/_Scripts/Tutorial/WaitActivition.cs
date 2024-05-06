using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitActivition : MonoBehaviour
{
    [SerializeField] float time;
    private GameObject t;
    
    void Start()
    {
        t=transform.GetChild(0).gameObject;
        t.SetActive(false);
        StartCoroutine(startAfert(time));
    }
    IEnumerator startAfert(float time)
    {
        yield return new WaitForSeconds(time);
        t.SetActive(true);

    }
   
}

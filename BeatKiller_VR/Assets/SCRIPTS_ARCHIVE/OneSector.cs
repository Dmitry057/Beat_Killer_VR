using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSector : MonoBehaviour
{
    InstantiateQuads IQ;
    public GameObject tonnelBase, decor;
    
    

    private void Start()
    {
        IQ = FindObjectOfType<InstantiateQuads>();
    }
    

    private void FixedUpdate()
    {
        if(transform.position.z < -70 || transform.position.z >70)
        {
            tonnelBase.SetActive(false);
            decor.SetActive(false);
        }
        else
        {
            tonnelBase.SetActive(true);
            decor.SetActive(true);
        }
       
    }
}

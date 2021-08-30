using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditorMode : MonoBehaviour
{
    

    private InstantiateQuads _instSector;
    private AudioClip _clip;

    
    private void Start()
    {
        _instSector = GetComponent<InstantiateQuads>();
        _clip = _instSector.clip;
    }
    
}

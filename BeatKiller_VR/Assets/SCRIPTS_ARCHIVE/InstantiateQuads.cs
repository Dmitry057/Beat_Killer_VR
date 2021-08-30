using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateQuads : MonoBehaviour
{
    [Header("Options")]
    public float distanceOffset;
    //public float bpm;
    public float startTime;
    public float speed;
    public bool canGo;
    public bool canEdit;
    private float distanceLevel;
    [Space(2)]
    [Header("Objects")]
    public GameObject[] Quads;
    public Transform ParentObj;
    [Space(2)]
    [Header("Music")]
    public AudioClip clip;
    private AudioSource _source;
    [Space(2)]
    [Header("UI Elements")]

    public GameObject startUI;
    public GameObject editorUI;
    [SerializeField] private Slider scrollBar;
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();

        startUI.SetActive(true);
        editorUI.SetActive(false);
    }
    public void CreateLVL(float bpm)
    {
        float allPm = bpm / 60 * clip.length;
        int bpmQuads = (int)((allPm + allPm % 4) / 4);
        distanceLevel = bpmQuads * distanceOffset;
        Debug.Log(bpmQuads + " = Sectors count");

        speed = bpm / 4 * distanceOffset / 60;

        for (int i = 0; i < bpmQuads; i++)
        {
            Vector3 offset = new Vector3(0, 0, -i * distanceOffset);
            GameObject sector = Instantiate(Quads[Random.Range(0, Quads.Length)], offset, Quaternion.identity);
            sector.transform.SetParent(ParentObj);
        }
    }
    public void EditorMode() 
    {
        canEdit = true;
        editorUI.SetActive(true);
        startUI.SetActive(false);
       // _source.Play();
    }
    public void PlayMode()
    {
        _source.clip = clip;
        _source.Play();

        editorUI.SetActive(false);
        startUI.SetActive(false);
        StartCoroutine(TimeChecker());
    }
    private IEnumerator TimeChecker()
    {
        yield return new WaitForSeconds(startTime);
        canGo = true;
    }
    private void FixedUpdate()
    {
        if (canGo)
        {
            ParentObj.position = Vector3.MoveTowards(ParentObj.position, new Vector3(0, 0, ParentObj.position.z + 1), Time.deltaTime * speed);
        }
        if (canEdit)
        {
            ParentObj.position = Vector3.Lerp(ParentObj.position, new Vector3(0, 0, scrollBar.value * distanceLevel), Time.deltaTime * speed);
        }
    }
    public void ScrollSlider()
    {
        _source.Play();
        _source.time = clip.length * scrollBar.value;
        canEdit = true;canGo = false;
    }
    public void ScrollExit(){_source.Stop();}
    public void playInEdit()
    {
        _source.time = clip.length * scrollBar.value;
        _source.Play();
        canEdit = false;
        canGo = true;
    }

}

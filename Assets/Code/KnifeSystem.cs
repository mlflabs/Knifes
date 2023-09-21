using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject knifePrefab;

    public Transform knifeStartLocation;

    public GameObject LevelParent;

    //Private
    private Knife currentKnife;
    private bool knifeLoaded;


    void Start()
    {
        createKnife();

    }

    private void throwListener()
    {
        Debug.Log("Throw Event");
        currentKnife.eventThrow.RemoveListener(throwListener);
        knifeLoaded = false;
        //createKnife();    
    }

    public void buyKnife()
    {
        if (knifeLoaded)
            return;

        createKnife();
    }

    private void createKnife()
    {
        var gb = Instantiate(knifePrefab, LevelParent.transform.position, Quaternion.identity);
        gb.transform.position = knifeStartLocation.position;

        currentKnife = gb.GetComponent<Knife>();
        currentKnife.eventThrow.AddListener(throwListener);

        knifeLoaded = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        //await Task.Yield();
        createKnife();    
    }

    public void buyKnife(int price = 2)
    {
        if (knifeLoaded)
            return;

        //do we have that much money
        //if (LevelStateSystem.Instance.credits < price)
        //    return;

        //LevelStateSystem.Instance.addCredits(-price);

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

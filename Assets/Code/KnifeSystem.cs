using Cysharp.Threading.Tasks;
using System;
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


    public static KnifeSystem Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }


    }



    void Start()
    {
        createKnife();

    }

    private async void throwListener()
    {
        Debug.Log("Throw Event");
        currentKnife.eventThrow.RemoveListener(throwListener);
        knifeLoaded = false;       
    }


    private void createKnife()
    {
        var gb = Instantiate(knifePrefab, LevelParent.transform.position, Quaternion.identity);
        gb.transform.position = knifeStartLocation.position;

        currentKnife = gb.GetComponent<Knife>();
        currentKnife.eventThrow.AddListener(throwListener);
        currentKnife.eventHit.AddListener(hitListener);

        knifeLoaded = true;
        print("Knife Build");
    }

    private void hitListener()
    {
        currentKnife.eventHit.RemoveListener(hitListener);
        if (LevelStateSystem.Instance.HasKnifes)
            createKnife();
    }
}

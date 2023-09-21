using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{

    //Public Variables
    public float rotationSpeed = 1.0f;
    public GameObject target;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.Rotate(0f,0f,rotationSpeed, Space.World);
    }
}

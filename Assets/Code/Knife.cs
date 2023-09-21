using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knife : MonoBehaviour
{

    [SerializeField]
    private Vector2 throwForce;


    //Events
    public UnityEvent eventThrow = new UnityEvent();

    //Private
    [SerializeField]
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D collider2d;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
    }


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isActive)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.excludeLayers = new LayerMask();


            Debug.Log("Mouse 0 Pressed");
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            
            eventThrow.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;

        isActive = false;

        if(collision.collider.tag == "Target")
        {
            Debug.Log("Hit Log");
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform.parent.transform);
        }

        if(collision.collider.tag == "Knife")
        {
            Debug.Log("Hit Knife");
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knife : MonoBehaviour
{

    [SerializeField]
    private Vector2 throwForce;

    [SerializeField]
    private int _scoreAmount = 1;

    //Events
    public UnityEvent eventThrow = new UnityEvent();

    //Private
    [SerializeField]
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D collider2d;

    private Shaker shaker;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
        shaker = GetComponent<Shaker>();
    }


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelStateSystem.Instance.LevelFinished) return;

        if(Input.GetMouseButtonDown(0) && isActive)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.excludeLayers = new LayerMask();


            //Debug.Log("Mouse 0 Pressed");
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            
            eventThrow.Invoke();
        }
    }

    public void DetatchFromTarget(){
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(null, false);
        //transform.SetParent(go.transform);
    }

    public void AttachToTarget(GameObject go){
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(go.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Danger")
        {
            //We went out of bounds, failed attempt
            //LevelStateSystem.Instance.AddCredits(1);
            Destroy(gameObject);
            return;
        }


        if (!isActive)
            return;

        isActive = false;

        if(collision.collider.tag == "Target")
        {
            Debug.Log("Hit Log");
            
            AttachToTarget(collision.collider.transform.parent.gameObject);
            
            Shaker shaker = collision.collider.transform.parent.gameObject.GetComponent<Shaker>();
            if (shaker){
                shaker.Shake();
            }

            Target target = collision.collider.transform.parent.gameObject.GetComponent<Target>();
            target.AddKnife(transform);

            LevelStateSystem.Instance.AddScore(_scoreAmount);

            LevelStateSystem.Instance.UseKnife();
        }
        else if(collision.collider.tag == "Knife")
        {
            Debug.Log("Hit Knife");
            //shake knife and let it fall also
            var otherGameObject = collision.collider.gameObject;
            var otherKnife = otherGameObject.GetComponent<Knife>();


            otherKnife.DetatchFromTarget();
            otherKnife.shaker.Shake();
            //shaker.Shake();

            LevelStateSystem.Instance.ThrowFailed();
        }
        else
        {
            Debug.Log("HIT:: + " + collision.collider.tag);
        }

    }
}


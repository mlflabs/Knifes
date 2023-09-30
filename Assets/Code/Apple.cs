using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private int _scoreAmount = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Knife")
        {
            Vector2 screenTopRight = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            var topRight = Camera.main.ScreenToWorldPoint(screenTopRight);
            print("TopRight:: " + topRight);
            transform.DOMove(topRight, .2f).OnComplete(()=>
            {
                LevelStateSystem.Instance.AddApple(_scoreAmount);
            });
        }
        else
        {
            Debug.Log("HIT:: + " + collider.tag);
        }

    }


}

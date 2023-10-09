using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    public int ScoreAmount = 10;

    private bool _appleHit;
    [SerializeField] private AudioClip _audioHit;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (_appleHit) return;

        if (collider.tag == "Knife")
        {
            AudioManager.Instance.PlaySound(_audioHit);
            Vector2 screenTopRight = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            var topRight = Camera.main.ScreenToWorldPoint(screenTopRight);
            print("TopRight:: " + topRight);
            transform.DOMove(topRight, .2f).OnComplete(()=>
            {
                LevelStateSystem.Instance.AddApple();
            });

            _appleHit = true;
        }
        else
        {
            Debug.Log("HIT:: + " + collider.tag);
        }

    }


}

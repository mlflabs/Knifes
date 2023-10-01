using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonZoomInOut : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleSize = new Vector3(1.5f, 1.5f, 1);
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(_scaleSize, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

   

}

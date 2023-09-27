using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.Restorer.Finder;
using DG.Tweening;
using UnityEngine;


[Serializable]
public class TargetMoveData
{
    public Vector2 StartPosition;
    public MoveStep[] Steps;

}



[Serializable]
public class MoveStep
{
    public float TimeInSeconds;
    public float RotateDestinationInDegrees;
    public float RotateSpeed;
    public float LevelRotateMultiplier = 1f;
    public Vector3 MoveDestination;
    public float MoveSpeed;
    public float LevelSpeedMultiplier;
}

public class Target : MonoBehaviour
{

    public TargetMoveData MoveData;
    private int _currentMoveDataStep = 0;
    private float _currentTime = 0;
    private MoveStep _moveData;
    void Start()
    {

        MoveTarget();
    }

    void MoveTarget()
    {
        _moveData = MoveData.Steps[_currentMoveDataStep];


        Sequence mySequence = DOTween.Sequence();

        if (_moveData.MoveDestination != Vector3.zero)
        {
            mySequence.Join(transform.DOMove(_moveData.MoveDestination, _moveData.TimeInSeconds)
                .SetRelative());
        }

        if (_moveData.RotateDestinationInDegrees != 0f)
        {
            mySequence.Join(transform.DORotate(new Vector3(0, 0, _moveData.RotateDestinationInDegrees),
                _moveData.TimeInSeconds,
                RotateMode.FastBeyond360)
            //.SetLoops(_moveData.RotateMultiplier, LoopType.Incremental)
            .SetRelative());
        }

        mySequence.OnComplete(() =>
        {

            _currentMoveDataStep = (_currentMoveDataStep + 1) % MoveData.Steps.Length;
            MoveTarget();
            print("Interval finished, moving to next step: " + _currentMoveDataStep);
        });


    }

    // Update is called once per frame
    void Update()
    {
        // _currentTime += Time.deltaTime;
        // if(_currentTime >= _moveData.TimeInSeconds){
        //     _currentMoveDataStep = (_currentMoveDataStep+1)%MoveData.Length;
        //     _moveData = MoveData[_currentMoveDataStep];
        // }

    }
}

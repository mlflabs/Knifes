using System;
using System.Collections.Generic;
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
    public float LevelDifficultyMultiplier = 1f;
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
    private MoveStep _moveData;

    [SerializeField] private AudioClip _audioBreak;

    [SerializeField] private GameObject _targetSprite;

    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private float _jumpXMultiplier = 2f;
    [SerializeField] private float _jumpDuration = 1f;


    [SerializeField] private GameObject parentPieces;
    [SerializeField] private GameObject parentItems;
    private List<Transform> _targetKnifes = new List<Transform>();

    void Start()
    {

        MoveTarget();
        LevelStateSystem.Instance.RegisterTarget(this);
        prepareObjects();


    }

    private void prepareObjects()
    {
        foreach (Transform t in parentPieces.transform)
        {
            t.gameObject.SetActive(false);

        }
    }

    Sequence _animationSequence;
    void MoveTarget()
    {
        _moveData = MoveData.Steps[_currentMoveDataStep];


        _animationSequence = DOTween.Sequence();

        if (_moveData.MoveDestination != Vector3.zero)
        {
            _animationSequence.Join(transform.DOMove(_moveData.MoveDestination,
                _moveData.TimeInSeconds - (GameManager.Instance.Data.Level * _moveData.LevelDifficultyMultiplier))
                .SetRelative());
        }

        if (_moveData.RotateDestinationInDegrees != 0f)
        {
            _animationSequence.Join(transform.DORotate(new Vector3(0, 0, _moveData.RotateDestinationInDegrees),
                _moveData.TimeInSeconds - (GameManager.Instance.Data.Level * _moveData.LevelDifficultyMultiplier),
                RotateMode.FastBeyond360)
            //.SetLoops(_moveData.RotateMultiplier, LoopType.Incremental)
            .SetRelative());
        }

        _animationSequence.OnComplete(() =>
        {

            _currentMoveDataStep = (_currentMoveDataStep + 1) % MoveData.Steps.Length;
            MoveTarget();
            print("Interval finished, moving to next step: " + _currentMoveDataStep);
        });


    }

    public void PlayDestroyTargetAnimation()
    {
        AudioManager.Instance.PlaySound(_audioBreak);
        _animationSequence?.Kill();
        foreach (Transform t in parentPieces.transform)
        {
            t.gameObject.SetActive(true);
            t.DOJump(t.position + new Vector3(t.position.x * _jumpXMultiplier, -10f, 1), _jumpPower, 1, _jumpDuration)
                .OnComplete(() => Destroy(t.gameObject));

        }

        foreach (Transform t in parentItems.transform)
        {
            t.gameObject.SetActive(true);
            t.DOJump(t.position + new Vector3(t.position.x * _jumpXMultiplier, -10f, 1), _jumpPower, 1, _jumpDuration)
                .OnComplete(() => Destroy(t.gameObject));

        }

        foreach (Transform t in _targetKnifes)
        {
            t.DOJump(t.position + new Vector3(t.position.x * _jumpXMultiplier, -10f, 1), _jumpPower, 1, _jumpDuration)
                .OnComplete(() => Destroy(t.gameObject));
        }

        _targetSprite.SetActive(false);


        //just destroy all the objectys
    }

    public void AddKnife(Transform knife)
    {
        _targetKnifes.Add(knife);
    }

    public void RemoveKnife(Transform knife)
    {
        _targetKnifes.Remove(knife);
    }
}

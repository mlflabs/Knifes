using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private int _obstaclesPerLevel;
    [SerializeField] private GameObject _obstaclesParent;
    [SerializeField] private float _jumpXMultiplier = 1f;
    [SerializeField] private float _jumpPower = 1f;

    [SerializeField] private float _jumpDuration = 1f;

    void Start()
    {
        LevelStateSystem.Instance.eventLevelFinished.AddListener(LevelCleared);
        DeactiveAllObstacles();
        ActivateObstacles();
    }

    void DeactiveAllObstacles()
    {
        foreach (Transform t in _obstaclesParent.transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    void ActivateObstacles()
    {
        var obstacleCount = GameManager.Instance.getLevel() * _obstaclesPerLevel;
        var childCount = _obstaclesParent.transform.childCount;
        var childArray = new List<int>();
        for (var i = 0; i < childCount - 1; i++)
        {
            childArray.Add(i);
        }

        for (var i = 0; i < obstacleCount; i++)
        {
            var rnd = UnityEngine.Random.Range(0, childArray.Count - 1);
            var obstacle = _obstaclesParent.transform.GetChild(rnd);
            obstacle.gameObject.SetActive(true);
            childArray.RemoveAt(i);
        }
    }

    void LevelCleared()
    {
        foreach (Transform t in _obstaclesParent.transform)
        {
            //t.gameObject.SetActive(true);
            if (t.gameObject.activeSelf)
                t.DOJump(t.position + new Vector3(t.position.x * _jumpXMultiplier, -10f, 1), _jumpPower, 1, _jumpDuration)
                    .OnComplete(() => Destroy(t.gameObject));

        }
    }

}


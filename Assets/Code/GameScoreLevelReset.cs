using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreLevelReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.setLevel(0);
        GameManager.Instance.setScore(0);
    }
}

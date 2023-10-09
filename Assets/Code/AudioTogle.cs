using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTogle : MonoBehaviour
{
    [SerializeField] private bool _togleMusic, _togleEffects;

    public void Toggle()
    {
        if(_togleEffects) AudioManager.Instance.ToggleEffects();
        if(_togleMusic) AudioManager.Instance.ToggleMusic();
    }
}

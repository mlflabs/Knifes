using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{

    [SerializeField] private AudioClip musicClip;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(musicClip);
    }
}

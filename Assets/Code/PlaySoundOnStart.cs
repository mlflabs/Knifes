using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        AudioManager.Instance.PlaySound(clip);
    }
}

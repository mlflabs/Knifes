using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        AudioManager.Instance.ChangeMasterVolume(_slider.value);    
        _slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));   
    }
}

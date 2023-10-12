using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class MainMenuSystem : MonoBehaviour
{

    [SerializeField] private Button _playButton;

    public static MainMenuSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }

    private void Start()
    {
        if (_playButton == null) return;

        _playButton.transform.DOScale(1.05f, 1)
            .SetLoops(-1, LoopType.Yoyo);
    }

}

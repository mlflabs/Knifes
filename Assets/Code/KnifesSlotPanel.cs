using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KnifesSlotPanel : MonoBehaviour
{

    [SerializeField] private Image _knifeIcon;
    [SerializeField] private Sprite _knifeFull;
    [SerializeField] private Sprite _knifeEmpty;

    private Image[] _knifeIcons;
    private int _usedKnifes = -1;

    // Start is called before the first frame update
    void Start()
    {
        prepareIcons();
        LevelStateSystem.Instance.eventKnifeThrown.AddListener(KnifeThrown);
    }

    private void KnifeThrown()
    {
        _usedKnifes++;

        _knifeIcons[_usedKnifes].sprite = _knifeEmpty;
    }

    private void prepareIcons()
    {
        _knifeIcons = new Image[LevelStateSystem.Instance.TotalKnifes];
        for (var i = 0; i < LevelStateSystem.Instance.TotalKnifes; i++)
        {
            var icon = Instantiate(_knifeIcon);
            icon.sprite = _knifeFull;
            icon.transform.SetParent(transform);
            _knifeIcons[i] = icon;

        }
    }
}

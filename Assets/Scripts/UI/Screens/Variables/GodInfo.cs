using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GodInfo : BasicScreen
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _quizButton;

    [SerializeField] private Image _godImage;
    [SerializeField] private Sprite[] _godImages;

    [SerializeField] private Image info;
    [SerializeField] private Planets[] _gods;
    [SerializeField] private Sprite[] _gosInfo;


    private Planets _currentGod;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        _quizButton.onClick.AddListener(Quiz);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
        _quizButton.onClick.RemoveListener(Quiz);
    }

    public void Init(Planets currentGod)
    {
        _currentGod = currentGod;
    }

    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    { 
        for(int i = 0; i < _gods.Length; i++)
        {
            if (_gods[i] == _currentGod)
            {
                _godImage.sprite = _godImages[i];
                info.sprite = _gosInfo[i];
            }
        }
    }   

    private void BackButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home); 
    }
    private void Quiz()
    {
        Quiz godInfo = (Quiz)UIManager.Instance.GetScreen(ScreenTypes.Quiz);
        godInfo.Init(_currentGod);
        UIManager.Instance.ShowScreen(ScreenTypes.Quiz);
    }
}
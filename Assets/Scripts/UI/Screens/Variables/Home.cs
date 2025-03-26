using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Home : BasicScreen
{
    [SerializeField] private AvatarManager avatarManager;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _infoGodButton;
    [SerializeField] private Button _quizButton;
    [SerializeField] private Button _factsButton;
    [SerializeField] private Button _galleryButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image _god;
    [SerializeField] private GameObject[] _godNames;
    [SerializeField] private Sprite[] _godImages;
    [SerializeField] private Gods[] gods;

    private int _currentGod;

    private void Start()
    {
        _currentGod = 0;

        _profileButton.onClick.AddListener(ProfileButton);
        _infoButton.onClick.AddListener(InfoButton);
        _infoGodButton.onClick.AddListener(GodInfoButton);
        _quizButton.onClick.AddListener(QuizButton);
        _factsButton.onClick.AddListener(FactButton);
        _galleryButton.onClick.AddListener(GalleryButton);
        _nextButton.onClick.AddListener(Next);
        _prevButton.onClick.AddListener(Prev);
    }
    private void OnDestroy()
    {
        _profileButton.onClick.RemoveListener(ProfileButton);
        _infoButton.onClick.RemoveListener(InfoButton);
        _infoGodButton.onClick.RemoveListener(GodInfoButton);
        _quizButton.onClick.RemoveListener(QuizButton);
        _factsButton.onClick.RemoveListener(FactButton);
        _galleryButton.onClick.RemoveListener(GalleryButton);
        _nextButton.onClick.RemoveListener(Next);
        _prevButton.onClick.RemoveListener(Prev);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        ConfigScreen();
    }

    private void ConfigScreen()
    {
        _name.text = PlayerPrefs.GetString("Name", "User Name");
        _coins.text = PlayerPrefs.GetInt("Coins").ToString();

        foreach (var god in _godNames)
        {
            god.SetActive(false);
        }

        for (int i = 0; i < gods.Length; i++)
        {
            if (gods[_currentGod] == gods[i])
            {
                _god.sprite = _godImages[i];
                _godNames[i].SetActive(true);
            }
        }
    }

    private void ProfileButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void InfoButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
    private void GodInfoButton()
    {
        GodInfo godInfo =  (GodInfo) UIManager.Instance.GetScreen(ScreenTypes.GodInfo);
        godInfo.Init(gods[_currentGod]);

        UIManager.Instance.ShowScreen(ScreenTypes.GodInfo);
    }
    private void QuizButton()
    {
        Quiz godInfo = (Quiz)UIManager.Instance.GetScreen(ScreenTypes.Quiz);
        godInfo.Init(gods[_currentGod]);
        UIManager.Instance.ShowScreen(ScreenTypes.Quiz);
    }
    private void FactButton()
    {
        GodFacts godInfo = (GodFacts)UIManager.Instance.GetScreen(ScreenTypes.Facts);
        godInfo.Init(gods[_currentGod]);
        UIManager.Instance.ShowScreen(ScreenTypes.Facts);
    }
    private void GalleryButton()
    {
        Gallery godInfo = (Gallery)UIManager.Instance.GetScreen(ScreenTypes.Gallery);
        godInfo.Init(gods[_currentGod]);
        UIManager.Instance.ShowScreen(ScreenTypes.Gallery);
    }
    private void Next()
    {
        if(_currentGod < gods.Length - 1)
            _currentGod++;

        ConfigScreen();
    }
    private void Prev()
    {
        if (_currentGod > 0)
            _currentGod--;

        ConfigScreen();
    }
}

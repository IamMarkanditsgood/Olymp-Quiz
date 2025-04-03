using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _infoButton;

    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _achievementsUnlocked;
    [SerializeField] private TMP_Text _quizezComplited;
    [SerializeField] private TMP_Text _totalCoins;

    [SerializeField] private Image _ahcieve;
    [SerializeField] private Sprite _openedAchieve;
    TextManager textManager = new TextManager();
    private void Start()
    {
     
        _name.text = PlayerPrefs.GetString("Name", "User Name");
        _homeButton.onClick.AddListener(HomeButton);
        _infoButton.onClick.AddListener(InfoButton);

    }
    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(HomeButton);
        _infoButton.onClick.RemoveListener(InfoButton);
    }

    public override void ResetScreen()
    {
        PlayerPrefs.SetString("Name", _name.text);
    }

    public override void SetScreen()
    {
        ConfigScreen();
    }

    private void ConfigScreen()
    {
        _name.text = PlayerPrefs.GetString("Name", "User Name");
        Debug.Log(_name);
        Debug.Log(PlayerPrefs.GetString("Name", "User Name"));
        _coins.text = PlayerPrefs.GetInt("Coins").ToString();
        _achievementsUnlocked.text = "0/4";
        textManager.SetText(PlayerPrefs.GetInt("Coins"), _totalCoins, true);
        _quizezComplited.text = PlayerPrefs.GetInt("QuizezCompeted").ToString();
        if (PlayerPrefs.HasKey("Achieve"))
        {
            _ahcieve.GetComponent<Image>().sprite = _openedAchieve;
            _achievementsUnlocked.text = "1/4";
        }
    }

    private void HomeButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void InfoButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }

}

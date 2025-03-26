using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    [SerializeField] private AvatarManager avatarManager;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _profileEditor;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image _ahcieve;
    [SerializeField] private Sprite _openedAchieve;

    private void Start()
    {


        _homeButton.onClick.AddListener(HomeButton);
        _infoButton.onClick.AddListener(InfoButton);
        _profileEditor.onClick.AddListener(ProfileButton);

    }
    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(HomeButton);
        _infoButton.onClick.RemoveListener(InfoButton);
        _profileEditor.onClick.RemoveListener(ProfileButton);
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

        if(PlayerPrefs.HasKey("Achieve"))
        {
            _ahcieve.sprite = _openedAchieve;
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

    private void ProfileButton()
    {
        UIManager.Instance.ShowPopup(PopupTypes.PlayerEditor);
    }
}

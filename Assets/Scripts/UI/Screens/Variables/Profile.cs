using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    [SerializeField] private AvatarManager avatarManager;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _galleryButton;
    [SerializeField] private Button _factButton;

    [SerializeField] private Button _profileEditor;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image[] _ahcieve;
    [SerializeField] private Sprite[] _openedAchieve;

    private void Start()
    {
        _homeButton.onClick.AddListener(HomeButton);
        _galleryButton.onClick.AddListener(GaleryButton);
        _factButton.onClick.AddListener(FactButton);

        _profileEditor.onClick.AddListener(ProfileButton);

    }
    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(HomeButton);
        _galleryButton.onClick.RemoveListener(GaleryButton);
        _factButton.onClick.RemoveListener(FactButton);

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

        if(PlayerPrefs.HasKey("Achieve1"))
        {
            _ahcieve[0].sprite = _openedAchieve[0];
        }
        if (PlayerPrefs.HasKey("Achieve2"))
        {
            _ahcieve[1].sprite = _openedAchieve[1];
        }
        if (PlayerPrefs.HasKey("Achieve3"))
        {
            _ahcieve[2].sprite = _openedAchieve[2];
        }
        if (PlayerPrefs.HasKey("Achieve4"))
        {
            _ahcieve[3].sprite = _openedAchieve[3];
        }
        if (PlayerPrefs.HasKey("Achieve5"))
        {
            _ahcieve[4].sprite = _openedAchieve[4];
        }
        if (PlayerPrefs.HasKey("Achieve6"))
        {
            _ahcieve[5].sprite = _openedAchieve[5];
        }
    }

    private void HomeButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void GaleryButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Gallery);
    }
    private void FactButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Facts);
    }

    private void ProfileButton()
    {
        UIManager.Instance.ShowPopup(PopupTypes.PlayerEditor);
    }
}

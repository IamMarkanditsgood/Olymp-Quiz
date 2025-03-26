using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEditor : BasicPopup
{
    [SerializeField] private AvatarManager avatarManager;

    [SerializeField] private TMP_InputField _name;
    [SerializeField] private Button _close;
    [SerializeField] private Button _save;
    [SerializeField] private Button _avatarButton;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _save.onClick.AddListener(Save);
        _avatarButton.onClick.AddListener(avatarManager.PickFromGallery);
    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _save.onClick.RemoveListener(Save);
        _avatarButton.onClick.RemoveListener(avatarManager.PickFromGallery);
    }

    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
        avatarManager.SetSavedPicture();
        _name.text = PlayerPrefs.GetString("Name", "User Name");
    }


    private void Close()
    {
        Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void Save()
    {
        avatarManager.Save();
        PlayerPrefs.SetString("Name", _name.text);
        Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
}

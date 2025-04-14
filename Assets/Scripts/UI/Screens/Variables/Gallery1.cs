using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gallery1 : BasicScreen
{
    public CharacterData[] characterDatas;

    public Button close;

    public Button home;
    public Button fact;
    public Button profile;

    [Serializable]
    public class CharacterData
    {
        public Planets character;
        public Image characterImage;
        public Sprite[] stateSprites;
        public Button _button;
        public GameObject popup;
    }

    public void Start()
    {
        for(int i = 0; i < characterDatas.Length; i++)
        {
            int index = i;
            characterDatas[index]._button.onClick.AddListener(() => Pressed(index));
        }

        close.onClick.AddListener(Close);

        home.onClick.AddListener(HomeButton);
        fact.onClick.AddListener(FactButton);
        profile.onClick.AddListener(ProfileButton);
    }

    public void OnDestroy()
    {
        for (int i = 0; i < characterDatas.Length; i++)
        {
            int index = i;
            characterDatas[index]._button.onClick.RemoveListener(() => Pressed(index));
        }

        close.onClick.RemoveListener(Close);

        home.onClick.RemoveListener(HomeButton);
        fact.onClick.RemoveListener(FactButton);
        profile.onClick.RemoveListener(ProfileButton);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        for (int i = 0; i < characterDatas.Length; i++)
        {
            string key = "Achieve" + (i+1);
            int amount = PlayerPrefs.GetInt(key);

            if (amount < characterDatas[i].stateSprites.Length)
            {
                characterDatas[i].characterImage.sprite = characterDatas[i].stateSprites[amount];
            }
        }
    }

    private void Pressed(int index)
    {
        close.gameObject.SetActive(true);
        characterDatas[index].popup.SetActive(true);    
    }

    private void Close()
    {
        close.gameObject.SetActive(false);

        for (int i = 0; i < characterDatas.Length; i++)
        {
            characterDatas[i].popup.SetActive(false);
        }
    }

    private void HomeButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void FactButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Facts);
    }
    private void ProfileButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
}
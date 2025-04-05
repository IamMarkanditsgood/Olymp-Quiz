using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : BasicScreen
{
    [Serializable]
    public class GodData
    {
        public Planets _god;
        public string _name;
        public Sprite[] _openImages;
        public Sprite[] _closedImages;
    }

    [SerializeField] private Button _backButton;

    [SerializeField] private TMP_Text _godName;
    [SerializeField] private TMP_Text _godName2;

    [SerializeField] private Image[] _images;
    [SerializeField] private Button[] _open;

    [SerializeField] private GodData[] _gods;
    private Planets _currentGod;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        
        for(int i = 0; i < _open.Length; i++)
        {
            int index = i;
            _open[index].onClick.AddListener(() => Open(index));

        }
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);

        for (int i = 0; i < _open.Length; i++)
        {
            int index = i;
            _open[index].onClick.RemoveListener(() => Open(index));

        }
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
        ConfigScreen();
    }

    private void ConfigScreen()
    {

        for (int i = 0; i < _gods.Length; i++)
        {
            if (_gods[i]._god == _currentGod)
            {
                for(int j = 0; j < _gods[i]._openImages.Length; j++)
                {
                    string key = _gods[i]._god + "art" + j;
                    
                    if (PlayerPrefs.GetInt(key) == 1)
                    {
                        _images[j].sprite = _gods[i]._openImages[j];
                    }
                    else
                    {
                        _images[j].sprite = _gods[i]._closedImages[j];
                    }
                }
            }
        }
    }


    private void Open(int Index)
    {
        if (PlayerPrefs.GetInt("Coins") >= 500)
        {
            for (int i = 0; i < _gods.Length; i++)
            {
                if (_gods[i]._god == _currentGod)
                {
                    string key = _gods[i]._god + "art" + Index;
                    PlayerPrefs.SetInt(key, 1);
                }
            }
            int newCoins = PlayerPrefs.GetInt("Coins");
            newCoins -= 500;
            PlayerPrefs.SetInt("Coins", newCoins);
            ConfigScreen();
        }
        else
        {
            UIManager.Instance.ShowPopup(PopupTypes.NoMoney);
        }
    }
    private void BackButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}

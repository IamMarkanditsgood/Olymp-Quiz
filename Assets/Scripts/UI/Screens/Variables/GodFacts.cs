using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodFacts : BasicScreen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _galleryButton;

    [SerializeField] private string[] _godfacts;
    [SerializeField] private Planets[] _gods;

    [SerializeField] private TMP_Text _factInfo;

    [SerializeField] private GameObject _factPanel;

    private const string LastClaimTimeKey = "LastClaimTime";
    private TimeSpan rewardCooldown = TimeSpan.FromHours(24);
    private Planets _currentGod;

    private void Start()
    {
        _homeButton.onClick.AddListener(HomeButton);
        _profileButton.onClick.AddListener(profileButton);
        _galleryButton.onClick.AddListener(GalleryButton);
    }
    private void Update()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;
    }
    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(HomeButton);
        _profileButton.onClick.RemoveListener(profileButton);
        _galleryButton.onClick.RemoveListener(GalleryButton);
    }

    public void Init(Planets currentGod)
    {
        
    }

    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;

        if (timeRemaining <= TimeSpan.Zero)
        {
            _factPanel.SetActive(true);
            GiveFact();
        }
     


    }

    private void GiveFact()
    {
        int randomFact = UnityEngine.Random.Range(0, _godfacts.Length);
        _factInfo.text = _godfacts[randomFact].ToString();
        PlayerPrefs.SetString(LastClaimTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();

    }

    private void HomeButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private DateTime GetLastClaimTime()
    {
        string lastClaimStr = PlayerPrefs.GetString(LastClaimTimeKey, string.Empty);
        return string.IsNullOrEmpty(lastClaimStr) ? DateTime.MinValue : DateTime.Parse(lastClaimStr);
    }
    private void profileButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void GalleryButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Gallery);
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodFacts : BasicScreen
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _okButton;

    [SerializeField] private string[] _godTitles;
    [TextArea(15, 20)]
    [SerializeField] private string[] _godfacts;
    [SerializeField] private string[] _godNames;
    [SerializeField] private Gods[] _gods;

    [SerializeField] private TMP_Text _factTitle;
    [SerializeField] private TMP_Text _factInfo;
    [SerializeField] private TMP_Text _godName;
    [SerializeField] private TMP_Text _godName2;

    [SerializeField] private GameObject _factPanel;
    [SerializeField] private GameObject _timerPanel;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _timer1;
    private const string LastClaimTimeKey = "LastClaimTime";
    private TimeSpan rewardCooldown = TimeSpan.FromHours(24);

    private Gods _currentGod;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        _okButton.onClick.AddListener(BackButton);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
        _okButton.onClick.RemoveListener(BackButton);
    }

    private void Update()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;
        _timer.text = $"{timeRemaining.Hours:D2}:{timeRemaining.Minutes:D2}:{timeRemaining.Seconds:D2}";
        _timer1.text = $"{timeRemaining.Hours:D2}:{timeRemaining.Minutes:D2}:{timeRemaining.Seconds:D2}";
    }

    public void Init(Gods currentGod)
    {
        _currentGod = currentGod;
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
            _timerPanel.SetActive(false);
            GiveFact();
        }
        else
        {
            
            _factPanel.SetActive(false);
            _timerPanel.SetActive(true);
        }


    }

    private void GiveFact()
    {
        for (int i = 0; i < _gods.Length; i++)
        {
            if (_gods[i] == _currentGod)
            {
                _godName.text = "Interesting Fact\r\nabout " + _godNames[i];
                _godName2.text = "Interesting Fact\r\nabout " + _godNames[i];

                _factTitle.text = _godTitles[i].ToString();
                _factInfo.text = _godfacts[i].ToString();
            }
        }

        PlayerPrefs.SetString(LastClaimTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private void BackButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private DateTime GetLastClaimTime()
    {
        string lastClaimStr = PlayerPrefs.GetString(LastClaimTimeKey, string.Empty);
        return string.IsNullOrEmpty(lastClaimStr) ? DateTime.MinValue : DateTime.Parse(lastClaimStr);
    }
}
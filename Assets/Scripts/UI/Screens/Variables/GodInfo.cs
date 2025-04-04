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
    
    [SerializeField] private string[] _godTitles;
    [TextArea(15, 20)]
    [SerializeField] private string[] _godInfos;
    [SerializeField] private Gods[] _gods;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private TMP_Text _title;

    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private Canvas Canvas;

    private Gods _currentGod;

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

    public void Init(Gods currentGod)
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
                _title.text = _godTitles[i];
                _info.text = _godInfos[i];
            }
        }

        _verticalLayoutGroup.enabled = false;
        _title.enabled = false;
        _info.enabled = false;
        StartCoroutine(ResetText());
    }   

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(0.1f);
        _verticalLayoutGroup.enabled = true;
        _title.enabled = true;
        _info.enabled = true;
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
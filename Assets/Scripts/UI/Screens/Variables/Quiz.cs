using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : BasicScreen
{
    [SerializeField] private TMP_Text _currentQuesstion;
    [SerializeField] private List<int> replies;


    [SerializeField] private Button[] _answersButton;
    [SerializeField] private Image[] _answers;
    [SerializeField] private TMP_Text[] _answerText;

    [SerializeField] private Sprite _defaultButton;
    [SerializeField] private Sprite _selecterdButton;
    [SerializeField] private Sprite _correctButton;
    [SerializeField] private Sprite _incorrectButton;

    [SerializeField] private TMP_Text _question;
    [SerializeField] private Button _reply;

    [SerializeField] private GodConfig[] _godconfigs;

    [SerializeField] private Button _close;

    [SerializeField] private TMP_Text _winResultText;
    [SerializeField] private Image[] _points;
    [SerializeField] private Sprite _defaultPoint;
    [SerializeField] private Sprite _currentPoint;



    private Gods _currentGod;
    private GodConfig currentGod;

    private int _currentQuestion;
    private int _coosedReply = -1;
    private bool _isWaitForReply;


    private int currentTime = 120;


    void Start()
    {
        _close.onClick.AddListener(Close);
        _reply.onClick.AddListener(Reply);

        for (int i = 0; i < _answersButton.Length; i++)
        {
            int index = i;
            _answersButton[index].onClick.AddListener(() => ChooseAnswer(index));
        }
    }

    void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _reply.onClick.RemoveListener(Reply);

        for (int i = 0; i < _answersButton.Length; i++)
        {
            int index = i;
            _answersButton[index].onClick.RemoveListener(() => ChooseAnswer(index));
        }
    }
    public void Init(Gods currentGod)
    {
        _currentGod = currentGod;
    }
    public override void SetScreen()
    {
        _reply.gameObject.SetActive(true);
        foreach (var godConfig in _godconfigs)
        {
            if (godConfig.types == _currentGod)
            {
                currentGod = godConfig;
            }
        }
        replies.Clear();
        _currentQuestion = 0;
        SetQuestion(); 
    }

    public override void ResetScreen()
    {
        StopAllCoroutines();
        
    }

    private void SetQuestion()
    {
        if (_currentQuestion < currentGod.godQuizzes.Length)
        {
            _isWaitForReply = true;
            foreach (var point in _points)
            {
                point.sprite = _defaultPoint;

            }
            foreach (var answer in _answers)
            {
                answer.sprite = _defaultButton;
            }
            _reply.interactable = false;
            _coosedReply = -1;
            SetPoints();
            _question.text = currentGod.godQuizzes[_currentQuestion].question;

            for (int i = 0; i < _answerText.Length; i++)
            {
                _answerText[i].text = currentGod.godQuizzes[_currentQuestion]._answers[i];
            }
        }
    }

    private void SetPoints()
    {
        _currentQuesstion.text = $"Question "+ (_currentQuestion + 1)+ "/" + (currentGod.godQuizzes.Length);
    }

    private void Reply()
    {
        _reply.gameObject.SetActive(false);
        bool isCorrect = CheckReply();
        _isWaitForReply = false;
        if (isCorrect)
        {
            _answers[_coosedReply].sprite = _correctButton;
            replies.Add(1);
        }
        else
        {
            _answers[_coosedReply].sprite = _incorrectButton;
            replies.Add(-1);
        }

        StartCoroutine(Next());
    }

    private IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        if (_currentQuestion + 1 == currentGod.godQuizzes.Length)
        {
            PlayerPrefs.SetInt("QuizezCompeted",( PlayerPrefs.GetInt("QuizezCompeted") + 1));
            int correct = 0;
            foreach (var answer in replies)
            {
                if (answer == 1)
                {
                    correct++;
                }
            }
            if (correct > _godconfigs.Length / 2)
            {
                int newScore = PlayerPrefs.GetInt("Coins");
                newScore += 100 * correct;
                _winResultText.text = "+" + (100 * correct);
                PlayerPrefs.SetInt("Coins", newScore);


                PlayerPrefs.SetInt("Achieve", 1);

                UIManager.Instance.ShowPopup(PopupTypes.QuizWin); 
            }
            else
            {
                UIManager.Instance.ShowPopup(PopupTypes.QuizLose);
            }
        }
        else
        {
            _reply.gameObject.SetActive(true);
            _currentQuestion++;
            SetQuestion();
        }
    }

    private void ChooseAnswer(int index)
    {
        if (_isWaitForReply)
        {
            foreach (var answer in _answers)
            {
                answer.sprite = _defaultButton;
            }
            _answers[index].sprite = _selecterdButton;
            _coosedReply = index;
            _reply.interactable = true;
            foreach(var point in _points)
            {
                point.sprite = _defaultPoint;

            }
            _points[index].sprite = _currentPoint;
        }
    }

    private bool CheckReply()
    {

        if (_coosedReply == currentGod.godQuizzes[_currentQuestion].correctReply)
        {
            return true;
        }
        return false;
    }

    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}

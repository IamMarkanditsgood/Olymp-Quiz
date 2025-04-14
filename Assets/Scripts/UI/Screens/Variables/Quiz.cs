using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : BasicScreen
{
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private Image[] _points;
    [SerializeField] private List<int> replies;

    [SerializeField] private Sprite _currentPoint;
    [SerializeField] private Sprite _defaultPoint;
    [SerializeField] private Sprite _corectPoint;
    [SerializeField] private Sprite _incorectPoint;

    [SerializeField] private Button[] _answersButton;
    [SerializeField] private Image[] _answers;
    [SerializeField] private TMP_Text[] _answerText;

    [SerializeField] private Sprite _defaultButton;
    [SerializeField] private Sprite _selecterdButton;
    [SerializeField] private Sprite _correctButton;
    [SerializeField] private Sprite _incorrectButton;

    [SerializeField] private TMP_Text _question;
    [SerializeField] private Button _reply;
    [SerializeField] private Button _next;

    [SerializeField] private GodConfig[] _godconfigs;

    [SerializeField] private Button _close;

    [SerializeField] private TMP_Text _winResultText;


    private Planets _currentGod;
    private GodConfig currentGod;

    private int _currentQuestion;
    private int _coosedReply = -1;
    private bool _isWaitForReply;


    private int currentTime = 120;


    void Start()
    {
        _close.onClick.AddListener(Close);
        _reply.onClick.AddListener(Reply);
        _next.onClick.AddListener(Next);

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
        _next.onClick.RemoveListener(Next);

        for (int i = 0; i < _answersButton.Length; i++)
        {
            int index = i;
            _answersButton[index].onClick.RemoveListener(() => ChooseAnswer(index));
        }
    }
    public void Init(Planets currentGod)
    {
        _currentGod = currentGod;
    }
    public override void SetScreen()
    {

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
        currentTime = 120;
        StartCoroutine(Timer());
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
            foreach (var answer in _answers)
            {
                answer.sprite = _defaultButton;
            }
            _reply.interactable = false;
            _next.gameObject.SetActive(false);
            _coosedReply = -1;
            SetPoints();
            _question.text = currentGod.godQuizzes[_currentQuestion].question;

            for (int i = 0; i < _answerText.Length; i++)
            {
                _answerText[i].text = currentGod.godQuizzes[_currentQuestion]._answers[i];
            }
        }
        else
        {
            int correctAnswers = 0;
            foreach (var reply in replies)
            {
                if (reply == 1)
                { correctAnswers++; }
            }
            StopAllCoroutines();
            int newScore = PlayerPrefs.GetInt("Coins");
            newScore += 500;
            PlayerPrefs.SetInt("Coins", newScore);

            if (correctAnswers > 5)
            {
                UIManager.Instance.ShowPopup(PopupTypes.QuizWin);
                _winResultText.text = "You answered " + correctAnswers + "/10\n" + "questions correctly!";
                switch (_currentGod)
                {
                    case Planets.Character1:
                        UpdateAchieve("Achieve1");
                        break;
                    case Planets.Character2:
                        UpdateAchieve("Achieve2");
                        break;
                    case Planets.Character3:
                        UpdateAchieve("Achieve3");
                        break;
                    case Planets.Character4:
                        UpdateAchieve("Achieve4");
                        break;
                    case Planets.Character5:
                        UpdateAchieve("Achieve5");
                        break;
                    case Planets.Character6:
                        UpdateAchieve("Achieve6");
                        break;
                }
            }
            else
            {
                UIManager.Instance.ShowPopup(PopupTypes.QuizLose);

            }
        }
    }

    private void UpdateAchieve(string key)
    {
        int amount = PlayerPrefs.GetInt(key);
        amount++;
        PlayerPrefs.SetInt(key, amount);
        PlayerPrefs.Save();
    }

    private void SetPoints()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if (_currentQuestion == i)
            {
                _points[i].sprite = _currentPoint;
            }
            else if (i > _currentQuestion)
            {
                _points[i].sprite = _defaultPoint;
            }
        }
        for (int i = 0; i < replies.Count; i++)
        {
            if (replies[i] == 1)
            {
                _points[i].sprite = _corectPoint;
            }
            else if (replies[i] == -1)
            {
                _points[i].sprite = _incorectPoint;
            }
        }
    }

    private void Reply()
    {
        bool isCorrect = CheckReply();
        _isWaitForReply = false;
        if (isCorrect)
        {
            _next.gameObject.SetActive(true);
            _answers[_coosedReply].sprite = _correctButton;
            _points[_currentQuestion].sprite = _corectPoint;
            replies.Add(1);
        }
        else
        {
            _next.gameObject.SetActive(true);
            _answers[_coosedReply].sprite = _incorrectButton;
            _points[_currentQuestion].sprite = _incorectPoint;
            replies.Add(-1);
        }
    }

    private void Next()
    {
        _currentQuestion++;
        SetQuestion();
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

    private IEnumerator Timer()
    {
        while (true)
        {

            _timer.text = currentTime.ToString();
            yield return new WaitForSeconds(1);
            currentTime--;
            if (currentTime == 0)
            {
                _timer.text = currentTime.ToString();
                UIManager.Instance.ShowPopup(PopupTypes.QuizLose);
                StopAllCoroutines();
            }
        }
    }
}

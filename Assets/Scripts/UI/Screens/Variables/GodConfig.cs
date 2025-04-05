using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "God", menuName = "ScriptableObjects/GodConfig", order = 1)]
public class GodConfig : ScriptableObject
{
    public Planets types;
    public string name;
    public GodQuiz[] godQuizzes;

    [Serializable]
    public class GodQuiz
    {
        public string question;
        public string[] _answers;
        public int correctReply;
    }
}
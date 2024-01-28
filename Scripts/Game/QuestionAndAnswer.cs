using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionAndAnswer
{
    public string Question;
    public List<string> Answers;
    public int CorrectAnswer; // Add this line to define the CorrectAnswer property
}


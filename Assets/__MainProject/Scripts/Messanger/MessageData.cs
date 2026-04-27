using UnityEngine;

[System.Serializable]
public class Answer
{
    public string Text;
    public bool IsAgree;
}

[System.Serializable]
public class MessageData
{
    public string AccountName;
    public string Text;
    public string Time;
    public Answer[] Answers;
}

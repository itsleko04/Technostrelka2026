using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Answer
{
    public string Text;
    public DialogueNode NextNode;
}

public class MessageData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Serialize(string text)
    {
        _text.text = text;
    }
}
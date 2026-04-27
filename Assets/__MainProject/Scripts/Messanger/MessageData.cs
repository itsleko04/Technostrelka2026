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
    [SerializeField] private TextMeshProUGUI _text, _name;
    [SerializeField] private Image _bg;

    public void Serialize(DialogueNode data)
    {
        _text.text = data.DialogueText;
        _bg.sprite = data.NodeBG;
    }
}
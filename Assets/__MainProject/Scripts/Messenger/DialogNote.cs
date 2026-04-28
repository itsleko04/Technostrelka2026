using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public bool IsSelf=true;

    [TextArea(3, 10)]
    public string DialogueText;

    [Header("Настройки ветвления")]
    public List<Answer> Answers;

    [Header("Автоматический переход (если нет ответов)")]
    public bool AutoNext;
    public float Delay = 2.0f;
    public DialogueNode NextNode;

    [Header("Финальный узел")]
    public bool IsEndNode;
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Messanger : MonoBehaviour
{
    [Header("Данные для работы мессенджера")]
    [SerializeField] private GameObject _messagePref;
    [SerializeField] private Transform _selfMessagesSpawn, _talker2MessagesSpawn;

    [SerializeField] private Transform _answersSpawn;
    [SerializeField] private GameObject _answerButtonPrefab;

    [Header("Просто удобные ивенты")]
    [SerializeField] private UnityEvent _onStart;

    [Header("Текст ожидание 'печатает...'")]
    [SerializeField] private Transform _waitForSendTxt;

    [Header("Заглушка для красивого отображения")]
    [SerializeField] private GameObject _decoy;

    [SerializeField] private UnityEvent OnBadEnd, OnGoodEnd;

    private void Start()
    {
        _onStart.Invoke();
    }

    public void StartDialogue(DialogueNode startNode)
    {
        ToNextNode(startNode);
    }

    IEnumerator WaitToNext(DialogueNode node)
    {
        SetWaitAnimState(true, node.IsSelf);
        ClearAnswers();
        yield return new WaitForSeconds(node.Delay);
        SetWaitAnimState(false, node.IsSelf);
        ToNextNode(node);
    }

    void ToNextNode(DialogueNode node)
    {
        Transform spawnPoint = node.IsSelf ? _selfMessagesSpawn : _talker2MessagesSpawn;

        if (node.AutoNext)
        {
            IEnumerator AutoNodeDisplay()
            {
                DisplayNode(node, spawnPoint);
                yield return new WaitForSeconds(node.Delay);
                StartCoroutine(WaitToNext(node.NextNode));
            }

            StartCoroutine(AutoNodeDisplay());
            return;
        }
        else if (node.IsEndNode)
        {
            DisplayNode(node, spawnPoint);
            EndDialogue(node);
            return;
        }

        ClearAnswers();
        
        DisplayNode(node, spawnPoint);

        foreach (var answer in node.Answers)
        {
            GameObject btnObj = Instantiate(_answerButtonPrefab, _answersSpawn);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = answer.Text;

            btnObj.GetComponent<Button>().onClick.AddListener(() => {
                if (answer.NextNode != null)
                {
                    DisplaySelfNode(answer.Text);
                    StartCoroutine(WaitToNext(answer.NextNode));
                }
                else
                    EndDialogue(node);
            });
        }
    }

    private void DisplayNode(DialogueNode node, Transform spawn)
    {
        Instantiate(_decoy, node.IsSelf ? _talker2MessagesSpawn : _selfMessagesSpawn);
        MessageData newMessage = Instantiate(_messagePref, spawn).GetComponent<MessageData>();
        newMessage.Serialize(node.DialogueText);
    }
    private void DisplaySelfNode(string text)
    {
        Instantiate(_decoy, _talker2MessagesSpawn);
        MessageData newMessage = Instantiate(_messagePref, _selfMessagesSpawn).GetComponent<MessageData>();
        newMessage.Serialize(text);
    }

    private void ClearAnswers()
    {
        foreach (Transform child in _answersSpawn) 
            Destroy(child.gameObject);
    }


    private void SetWaitAnimState(bool state, bool isSelf)
    {
        Transform spawnPoint = isSelf ? _selfMessagesSpawn : _talker2MessagesSpawn;
        _waitForSendTxt.SetParent(spawnPoint);
        _waitForSendTxt.SetAsLastSibling();
        _waitForSendTxt.gameObject.SetActive(state);
        _waitForSendTxt.TryGetComponent(out WaitForMessageIdle animator);
        if (state)
            animator.StartAnim();
        else
            animator.StopAnim();
    }

    void EndDialogue(DialogueNode lastNode)
    {
        Debug.Log("Диалог окончен");
        if (!lastNode.IsEndNode)
            return;

        if(lastNode.IsGoodEnd)
            OnGoodEnd.Invoke();
        else
            OnBadEnd.Invoke();
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Messanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueTextField;
    [SerializeField] private Transform _choiceRoot;
    [SerializeField] private GameObject _choiceButtonPrefab;

    public void StartDialogue(DialogueNode startNode)
    {
        DisplayNode(startNode);
    }

    void DisplayNode(DialogueNode node)
    {
        if(node.IsEndNode)
        {
            EndDialogue();
            return;
        }

        if (node.AutoNext)
        {
            IEnumerator WaitToNext()
            {
                yield return new WaitForSeconds(node.Delay);

                DisplayNode(node.NextNode);
            }

            StartCoroutine(WaitToNext());
            return;
        }

        foreach (Transform child in _choiceRoot) Destroy(child.gameObject);

        _dialogueTextField.text = node.DialogueText;

        foreach (var answer in node.Answers)
        {
            GameObject btnObj = Instantiate(_choiceButtonPrefab, _choiceRoot);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = answer.Text;

            btnObj.GetComponent<Button>().onClick.AddListener(() => {
                if (answer.NextNode != null)
                    DisplayNode(answer.NextNode);
                else
                    EndDialogue();
            });
        }
    }

    void EndDialogue()
    {
        _dialogueTextField.text = "...";
        foreach (Transform child in _choiceRoot) Destroy(child.gameObject);
        Debug.Log("Диалог окончен");
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(TutorialController))]
public class TutorialControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        TutorialController myTarget = (TutorialController)target;

        if (GUILayout.Button("К предыдущему этапу"))
        {
            myTarget.ToPreviousStep();
        }
        if (GUILayout.Button("К следующему этапу"))
        {
            myTarget.ToNextStep();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Создать новый этап"))
        {
            myTarget.CreateNewStep();
        }
    }
}
#endif
public class TutorialController : MonoBehaviour
{
    private int _stepIndex;
    [SerializeField] private List<GameObject> _steps = new List<GameObject>();

    private void Awake()
    {
        SetStep(_stepIndex);
    }

    private void SetStep(int stepIndex)
    {
        foreach (GameObject step in _steps)
        {
            step.SetActive(false);
        }
        _steps[stepIndex].SetActive(true);
    }

    public void ToPreviousStep()
    {
        _stepIndex--;
        _stepIndex = Mathf.Clamp(_stepIndex, 0, _steps.Count - 1);
        SetStep(_stepIndex);
    }

    public void ToNextStep()
    {
        _stepIndex++;
        _stepIndex = Mathf.Clamp(_stepIndex, 0, _steps.Count - 1);
        SetStep(_stepIndex);
    }

    internal void CreateNewStep()
    {
        GameObject newStep = new GameObject($"Step{_steps.Count + 1}");
        newStep.transform.parent = transform;
        newStep.transform.localPosition = Vector3.zero;
        _steps.Add(newStep);
    }
}

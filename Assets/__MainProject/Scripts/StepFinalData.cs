using UnityEngine;

[CreateAssetMenu(fileName = "FinalData", menuName = "ILT/FinalData")]
public class StepFinalData : ScriptableObject
{
    [TextArea(3, 6)]
    public string Descryption;
}

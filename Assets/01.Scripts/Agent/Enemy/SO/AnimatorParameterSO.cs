using UnityEngine;

[CreateAssetMenu(fileName = "AnimaitonParameterSO", menuName = "SO/Animator/AnimaitonParameterSO")]
public class AnimatorParameterSO : ScriptableObject
{
    public string parameterName;
    public int hashValue;

    private void OnValidate()
    {
        hashValue = Animator.StringToHash(parameterName);
    }
}

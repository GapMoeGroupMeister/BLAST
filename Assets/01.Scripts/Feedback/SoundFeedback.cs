using Crogen.ObjectPooling;
using SoundManage;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO _soundData;

    public override void CreateFeedback()
    {
        SoundObject sound = gameObject.Pop(PoolType.SFXPlayer, transform.position, Quaternion.identity) as SoundObject;
        sound.PlaySound(_soundData);
    }

    public override void FinishFeedback()
    {
    }
}
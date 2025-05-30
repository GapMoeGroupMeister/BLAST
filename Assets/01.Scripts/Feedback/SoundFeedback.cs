﻿using Crogen.CrogenPooling;
using SoundManage;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO _soundData;

    public override void CreateFeedback()
    {
        SoundObject sound = gameObject.Pop(SoundPoolType.SFXPlayer, transform.position, Quaternion.identity) as SoundObject;
        if(sound == null) 
        {
            print("Sound Null");
            return;
        }
        sound.PlaySound(_soundData);
    }

    public override void FinishFeedback()
    {
    }
}
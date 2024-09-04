using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackToPlay;

    private void Awake()
    {
        _feedbackToPlay = GetComponents<Feedback>().ToList();
        
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        _feedbackToPlay.ForEach(f => f.CreateFeedback());

    }

    public void FinishFeedback()
    {
        _feedbackToPlay.ForEach(f => f.FinishFeedback());
    }
}
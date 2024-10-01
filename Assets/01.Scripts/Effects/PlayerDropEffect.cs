using UnityEngine;

public class PlayerDropEffect : MonoBehaviour
{
    private ParticleSystem _vfx;
    private FeedbackPlayer _soundFeedback;

    private void Awake()
    {
        _vfx = GetComponentInChildren<ParticleSystem>();
        _soundFeedback = GetComponentInChildren<FeedbackPlayer>();
        
    }


    public void Play()
    {
        _vfx.Play();
        _soundFeedback.PlayFeedback();


    }

}

using UnityEngine;

public class VFXPlayFeedback : Feedback
{
    [SerializeField] private ParticleSystem[] _particles;

    public override void CreateFeedback()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }
    }

    public override void FinishFeedback()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Stop();
        }
    }
}
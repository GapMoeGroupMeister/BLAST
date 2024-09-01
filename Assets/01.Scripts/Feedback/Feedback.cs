using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    protected Agent _owner;

    protected virtual void Awake()
    {
        _owner = transform.parent.GetComponent<Agent>();
    }

    protected void OnDisable()
    {
        FinishFeedback();
    }
}
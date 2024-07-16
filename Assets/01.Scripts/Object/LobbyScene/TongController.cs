using UnityEngine;

public class TongController : MonoBehaviour
{
    private Animator _animator;
    private int _grabHash;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _grabHash = Animator.StringToHash("IsGrab");
    }

    [ContextMenu("Debug_Grab")]
    private void Grab()
    {
        SetGrab(true);
    }

    [ContextMenu("Debug_Release")]
    private void Release()
    {
        SetGrab(false);
    }
    
    public void SetGrab(bool value)
    {
        _animator.SetBool(_grabHash, value);
    }
    
    
}

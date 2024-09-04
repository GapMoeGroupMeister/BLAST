using UnityEngine;
using UnityEngine.UI;

public class ExpGauge : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;

    public void HandleRefreshEvent(float fill)
    {
        _gaugeFill.fillAmount = fill;
    }
    
    
}

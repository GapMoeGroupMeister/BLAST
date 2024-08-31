using UnityEngine;
using UnityEngine.UI;

public class ExpGauge : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;

    public void HandleRefreshEvent(int value, int max)
    {
        _gaugeFill.fillAmount = (float)value / max;
    }
    
    
}

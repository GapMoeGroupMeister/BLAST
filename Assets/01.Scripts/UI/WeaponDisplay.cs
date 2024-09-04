using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _overloadColor;

    

    /**
     * <summary>
     * 게이지를 현재 과부하 정도에 따라 갱신해주는 메서드
     * </summary>
     */
    public void HandleDisplayRefresh(int currentOverload, int maxOverload)
    {
        float ratio = (float)currentOverload / maxOverload;
        _gaugeImage.fillAmount = ratio;
        _gaugeImage.color = Color.Lerp(_normalColor, _overloadColor, ratio);
        

    }

    private void RefreshText()
    {
        // 과부하 %를 표기시 구현
    }
}

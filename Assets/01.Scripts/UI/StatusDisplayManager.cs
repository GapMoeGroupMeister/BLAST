using DG.Tweening;
using UnityEngine;

public class StatusDisplayManager : MonoBehaviour
{
    [SerializeField] private WeaponDisplay _weaponDisplay_Left;
    [SerializeField] private WeaponDisplay _weaponDisplay_Right;
    [SerializeField] private HealthUI _healthUI; 
        
    private PlayerPart _currentPart;
    private CanvasGroup _canvasGroup;    
    
    private void Start()
    {
        _currentPart = PlayerPartController.Instance.currentPlayerPart;
        _currentPart.magazineInfoL.playerOverloadEvent += _weaponDisplay_Left.HandleDisplayRefresh;
        _currentPart.magazineInfoR.playerOverloadEvent += _weaponDisplay_Right.HandleDisplayRefresh;
        GameManager.Instance.Player.HealthCompo.OnHealthChangedEvent.AddListener(_healthUI.Refresh);
        // 체력 이벤트 등록하기
    }

    public void SetVisible(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
        _canvasGroup.DOFade(value ? 1f : 0f, 0.2f);
    }
    
}

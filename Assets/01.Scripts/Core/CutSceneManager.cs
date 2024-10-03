using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private GameUICanvas _gameCanvas;
    [SerializeField] private CinematicPanel _cinematicPanel; // 상하 잘리는 시네마틱 효과 패널
    [SerializeField] private Player _player;
    [SerializeField] private FollowToTarget _playerbase;
    private Transform _playerTrm;
    [Header("GameStart CutScene")]
    [SerializeField] private float _fallHeight = 300f;
    [SerializeField] private PlayerDropEffect _landingEffect;
    [SerializeField] private float _zoomOutSize = 35f;
    [SerializeField] private float _zoomOutDuration = 3f;
    private Transform _landingVFXTrm;

    private void Awake()
    {
        _landingVFXTrm = _landingEffect.transform;
    }

    public void Init(Player player)
    {
        _player = player;
        _playerTrm = _player.transform;
    }

    private void PlayerSetMovementOption(bool canMove)
	{
        var movent = (_player.MovementCompo as PlayerMovement);
        movent.SetCanMove(canMove);
        _player.currentPlayerPart.magazineInfoL.CanAttack = canMove;
        _player.currentPlayerPart.magazineInfoR.CanAttack = canMove;
    }

    public void PlayGameStartCutScene()
    {
        StartCoroutine(GameStartCutSceneCoroutine());

    }

    private IEnumerator GameStartCutSceneCoroutine()
    {
        PlayerSetMovementOption(false);
        Vector3 pos = new Vector3(0f, 0f, 0f);// 나중에 이를 스테이지 시작위치로 변경해야한다.
        _landingVFXTrm.position = pos;
        _playerTrm.position = pos + new Vector3(0, _fallHeight, 0);
        _playerTrm.DOMoveY(0f, 5f).SetEase(Ease.InExpo).OnComplete(() => _landingEffect.Play());
        _gameCanvas.Close();
        _cinematicPanel.ShowStartCutScene();
        CameraShakeController.Instance.ShakeCam(1f, 4.5f);
        ZoomController.Instance.ForceZoomOut(_zoomOutSize, _zoomOutDuration, 1f);
        yield return new WaitForSeconds(5f);
        CameraShakeController.Instance.ShakeCam(20f, 0.35f);
        _gameCanvas.Open();
        PlayerSetMovementOption(true);
    }
}

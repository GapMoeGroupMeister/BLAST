using Crogen.PowerfulInput;
using UnityEngine;
using Cinemachine;
using System.Collections;
using DG.Tweening;

public class ZoomController : MonoSingleton<ZoomController>
{
	[SerializeField] private CinemachineVirtualCamera _virtualCamera;
	private InputReader _inputReader;
	[SerializeField] private float _minFOV = 10f;
	[SerializeField] private float _maxFOV = 50f;
	protected override void Awake()
	{
		base.Awake();	
		_inputReader = GameManager.Instance.InputReader;
	}

    private void OnEnable()
    {
        _inputReader.ZoomEvent += HandleZoom;
    }

    private void OnDestroy()
	{
		_inputReader.ZoomEvent -= HandleZoom;
	}

	private void HandleZoom(float axis)
	{
		_virtualCamera.m_Lens.FieldOfView -= axis;
		_virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(_virtualCamera.m_Lens.FieldOfView, _minFOV, _maxFOV);
	}

	public void ForceZoomOut(float orthographicSize, float time, float duration)
	{
		Sequence seq = DOTween.Sequence();

		float originSize = 20f;

		seq.OnStart(() =>
		{
			_inputReader.ZoomEvent -= HandleZoom;
		});

		seq.Append(DOTween.To(() => _virtualCamera.m_Lens.OrthographicSize,
			x => _virtualCamera.m_Lens.OrthographicSize = x,
			orthographicSize, time));

		seq.AppendInterval(duration);

		seq.Append(DOTween.To(() => _virtualCamera.m_Lens.OrthographicSize,
			x => _virtualCamera.m_Lens.OrthographicSize = x,
			originSize, time));


		seq.OnComplete(() =>
		{
			_inputReader.ZoomEvent += HandleZoom;
		});
	}
}
using Crogen.PowerfulInput;
using UnityEngine;
using Cinemachine;
using System.Collections;
using DG.Tweening;

public class ZoomController : MonoSingleton<ZoomController>
{
	[SerializeField] private CinemachineVirtualCamera _virtualCamera;
	private InputReader _inputReader;

	private void Awake()
	{
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
		_virtualCamera.m_Lens.OrthographicSize -= axis;
		_virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(_virtualCamera.m_Lens.OrthographicSize, 20, 60);
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
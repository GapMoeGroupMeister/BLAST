using Crogen.PowerfulInput;
using UnityEngine;
using Cinemachine;
public class ZoomController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _virtualCamera;
	private InputReader _inputReader;
	
	private void Awake()
	{
		_inputReader = GameManager.Instance.InputReader;

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
}

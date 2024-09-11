using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoSingleton<CameraShakeController>
{
    [SerializeField]private CinemachineVirtualCamera _playerCam;
    private CinemachineBasicMultiChannelPerlin _perlin;

    private float _power, _duration;
    private Coroutine _shakeRoutine;

    private void Awake()
    {
        _perlin = _playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ShakeCam(10, 0.1f);
        }
    }

    public void ShakeCam(float power, float duration)
    {
        _power = power;
        _duration = duration;

        if (_shakeRoutine != null)
            StopCoroutine(_shakeRoutine);

        _shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        _perlin.m_AmplitudeGain = _power;
        _perlin.m_FrequencyGain = _power;

        yield return new WaitForSeconds(_duration);

        _perlin.m_AmplitudeGain = 0;
        _perlin.m_FrequencyGain = 0;
    }
}

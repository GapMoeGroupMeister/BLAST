using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
	private float _defaultTimeScale = 1f;
	public float GetDefaultTimeScale => _defaultTimeScale;

	private void Awake()
	{
		PlayTime();
	}

	public void SetDefaultTimeScale(float timescale)
	{
		_defaultTimeScale = timescale;
	}

	public void PlayTime()
	{
		Time.timeScale = _defaultTimeScale;
	}

	public void PauseTime()
	{
		Time.timeScale = 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
	private float _defaultTimeScale = 1f;
	public float GetDefaultTimeScale => _defaultTimeScale;
	private float _currentGlobalTimer = 0;
	public float CurrentGlobalTimer
	{
		get => _currentGlobalTimer;
		set
		{
			_currentGlobalTimer = value;

			_timeText.text = CurrentGlobalTimerString;
		}
	}
	public string CurrentGlobalTimerString 
	{
		get {
			
			int min = (int)_currentGlobalTimer / 60;
			int sec = (int)_currentGlobalTimer % 60;

			return $"{min.ToString("00")} : {sec.ToString("00")}";
		}
	}

	[SerializeField] private TextMeshProUGUI _timeText;

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

	private void Update()
	{
		CurrentGlobalTimer += Time.deltaTime;
	}
}

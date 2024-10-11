using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private static float _defaultTimeScale = 1f;
	public static float GetDefaultTimeScale => _defaultTimeScale;
	private static float _currentGlobalTimer = 0;

	private float CurrentGlobalTimer
	{
		get => _currentGlobalTimer;
		set
		{
			_currentGlobalTimer = value;

			_timeText.text = CurrentGlobalTimerString;
		}
	}
	public static string CurrentGlobalTimerString 
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
		_currentGlobalTimer = 0f;
		SetDefaultTimeScale(1f);
		PlayTime();
	}

	public static void SetDefaultTimeScale(float timescale)
	{
		_defaultTimeScale = timescale;
	}

	public static void PlayTime()
	{
		Time.timeScale = _defaultTimeScale;
	}

	public static void PauseTime()
	{
		Time.timeScale = 0;
	}

	private void Update()
	{
		CurrentGlobalTimer += Time.deltaTime;
	}
}

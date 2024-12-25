using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private static float _defaultTimeScale = 1f;
	public static float GetDefaultTimeScale => _defaultTimeScale;
	public static float CurrentGlobalTimer { get; private set; }
	
	public static string CurrentGlobalTimerString 
	{
		get {
			
			int min = (int)CurrentGlobalTimer / 60;
			int sec = (int)CurrentGlobalTimer % 60;

			return $"{min:00} : {sec:00}";
		}
	}


	private void Awake()
	{
		CurrentGlobalTimer = 0f;
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

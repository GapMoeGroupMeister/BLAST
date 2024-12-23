using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DieContainer : MonoBehaviour
{
	[SerializeField] private DieButtonPanel _dieInfoContainer;
	[SerializeField] private TextMeshProUGUI _timeText;
	[SerializeField] private TextMeshProUGUI _coinText;

	//Components
	private CanvasGroup _canvasGroup;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void ShowPanel(float duration = 0.4f)
	{
		ResourceManager.Instance.SaveCoin();
		_timeText.text = TimeManager.CurrentGlobalTimerString;
		_coinText.text = ResourceManager.Instance.GetCoin().ToString();
		Sequence seq = DOTween.Sequence().SetUpdate(true);
		seq.Append(_canvasGroup.DOFade(1, duration).SetUpdate(true));
		seq.AppendCallback(() =>
		{
			TimeManager.PauseTime();
			_canvasGroup.interactable = true;
			_canvasGroup.blocksRaycasts = true;
		});
		seq.Append(_dieInfoContainer.ShowButtonPanel());
	}
}

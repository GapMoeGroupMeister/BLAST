using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DieContainer : MonoBehaviour
{
	[SerializeField] private DieButtonPanel _dieInfoContainer;

	//Components
	private CanvasGroup _canvasGroup;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void ShowPanel(float duration = 0.4f)
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(_canvasGroup.DOFade(1, duration));
		seq.AppendCallback(() =>
		{
			_canvasGroup.interactable = true;
			_canvasGroup.blocksRaycasts = true;
			_dieInfoContainer.ShowButtonPanel();
		});
	}
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieButtonGroup : MonoBehaviour
{
	private CanvasGroup _canvasGroup;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public Sequence ShowButtons()
	{
		Sequence seq = DOTween.Sequence();

		seq.Append(_canvasGroup.DOFade(1, 0.1f)).SetUpdate(true);
		seq.AppendCallback(() =>
		{
			_canvasGroup.interactable = true;
			_canvasGroup.blocksRaycasts = true;
		}).SetUpdate(true);
		seq.Append((_canvasGroup.transform as RectTransform)?.DOScale(1f, 1.1f)).SetUpdate(true);

		return seq;
	}

	public void OnRetry()
	{
		SceneLoadingManager.LoadScene();
	}

	public void OnContinue()
	{
		SceneLoadingManager.LoadScene("LobbyScene");
	}
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInfoContainer : MonoBehaviour
{
	[SerializeField] private RectTransform _dieTextTrm;
	[SerializeField] private float _dieTextPosY = 42.5f;
	[SerializeField] private RectTransform _btnGroupTrm;

	[ContextMenu("ShowButtons")]
	public void ShowButtons()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(_dieTextTrm.DOAnchorPosY(_dieTextPosY, 1.6f));
		seq.Append(_btnGroupTrm.DOScale(1f, 1.1f));
	}
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieButtonPanel : MonoBehaviour
{
	[SerializeField] private RectTransform _dieTextTrm;
	[SerializeField] private float _dieTextPosY = 42.5f;
	[SerializeField] private DieButtonGroup _dieButtonGroup;

	[ContextMenu("ShowButtonPanel")]
	public void ShowButtonPanel()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(_dieTextTrm.DOAnchorPosY(_dieTextPosY, 1.6f));
		seq.Append(_dieButtonGroup.ShowButtons());
	}
}

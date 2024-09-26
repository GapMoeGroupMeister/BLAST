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
	public Sequence ShowButtonPanel()
	{
		Sequence seq = DOTween.Sequence().SetUpdate(true);
		seq.Append(_dieTextTrm.DOAnchorPosY(_dieTextPosY, 1.6f)).SetUpdate(true);
		seq.Append(_dieButtonGroup.ShowButtons());

		return seq;
	}
}

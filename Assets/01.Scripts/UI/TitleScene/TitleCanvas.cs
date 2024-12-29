using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BLAST.TitleScene
{
    public class TitleCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _playText;
        
        private void Awake()
        {
            _titleText.rectTransform.localScale = new Vector3(0.5f, _titleText.rectTransform.localScale.y, _titleText.rectTransform.localScale.z);
            _titleText.alpha = 0;
            _playText.alpha = 0;
        }

        private void Start()
        {
            Sequence seq = DOTween.Sequence();
            seq
                .AppendInterval(1)
                .Append(_titleText.DOFade(1, 2f))
                .Join(_titleText.rectTransform.DOScaleX(1, 2f))
                .Append(_playText.DOFade(1, 1.5f))
                .AppendInterval(1)
                .AppendCallback(() =>
                {
                    TitleSceneController.CanMoveToOtherScene = true;
                });
        }
    }
}
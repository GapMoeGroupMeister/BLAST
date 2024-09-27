using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CinematicPanel : UIPanel
{
    [SerializeField] private Image _edgeUpImage;
    [SerializeField] private Image _edgeDownImage;

    [Header("Setting Values")]
    [SerializeField] private float _startDelay = 1f;    
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    [ContextMenu("CutScene")]
    public void ShowStartCutScene()
    {
        _edgeUpImage.fillAmount = 1f;
        _edgeDownImage.fillAmount = 1f;
        StartCoroutine(CutSceneCoroutine());
    }

    private IEnumerator CutSceneCoroutine()
    {
        yield return new WaitForSeconds(_startDelay);
         _edgeUpImage.fillAmount = 0.5f;
        _edgeDownImage.fillAmount = 0.5f;
        _edgeUpImage.DOFillAmount(0f, _duration).SetEase(_ease);
        _edgeDownImage.DOFillAmount(0f, _duration).SetEase(_ease);

    }

    public void OpenCinematicEffect(float duration){
        _edgeUpImage.DOFillAmount(0.1f, duration).SetUpdate(true);
    }


    public void CloseCinematicEffect(float duration){
        _edgeUpImage.DOFillAmount(0f, duration).SetUpdate(true);
    }



}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private Image _fadeImage;
    
    private void Awake()
    {
        _fadeImage = GetComponent<Image>();
    }
    
    public void Fade(float duration, float targetAlpha, TweenCallback callback = null)
    {
        _fadeImage.DOFade(targetAlpha, duration).OnComplete(callback);
    }
}

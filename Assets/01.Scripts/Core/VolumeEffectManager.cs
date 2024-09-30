using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeEffectManager : MonoSingleton<VolumeEffectManager>
{

    [SerializeField] private Volume _globalVolume;
    private ColorAdjustments colorAd;
    private ChromaticAberration chromaticAb;
    private bool _isChomaEffect;
    private bool _isColorAdEffect;
    [Header("Effect Setting")]
    [SerializeField] private float _colorAdScale;
    [SerializeField] private float _chromaticAbScale;
    [SerializeField] private float _duration;
    [SerializeField] private float _deltaDuration;


    private void Awake()
    {
        _globalVolume.profile.TryGet(out colorAd);
        _globalVolume.profile.TryGet(out chromaticAb);

    }

    #region Volume Setting Function


    public void SetGrayScale(float intensity, float duration, float moveDuration)
    {
        if (_isColorAdEffect) return;
        StartCoroutine(GrayScaleCoroutine(intensity, duration, moveDuration));
    }

    private IEnumerator GrayScaleCoroutine(float intensity, float duration, float moveDuration)
    {
        _isColorAdEffect = true;
        float currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            colorAd.saturation.value = Mathf.Lerp(0, intensity, currentTime / moveDuration);
            yield return null;
        }
        colorAd.saturation.value = intensity;
        yield return new WaitForSeconds(duration);
        currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            colorAd.saturation.value = Mathf.Lerp(intensity, 0, currentTime / moveDuration);
            yield return null;
        }
        _isColorAdEffect = false;
        colorAd.saturation.value = 0;
    }

    public void SetChromaticAb(float intensity, float duration, float moveDuration)
    {
        if (_isChomaEffect) return;
        StartCoroutine(ChromaticAbCoroutine(intensity, duration, moveDuration));
    }

    private IEnumerator ChromaticAbCoroutine(float intensity, float duration, float moveDuration)
    {
        _isChomaEffect = true;
        float currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            chromaticAb.intensity.value = Mathf.Lerp(0, intensity, currentTime / moveDuration);
            yield return null;
        }
        chromaticAb.intensity.value = intensity;
        yield return new WaitForSeconds(duration);
        currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            chromaticAb.intensity.value = Mathf.Lerp(intensity, 0, currentTime / moveDuration);
            yield return null;
        }
        chromaticAb.intensity.value = 0;
        _isChomaEffect = false;
    }

    #endregion


    #region 

    public void HandlePlayerHit()
    {
        SetGrayScale(_colorAdScale, _duration, _deltaDuration);
        SetChromaticAb(_chromaticAbScale, _duration, _deltaDuration);

    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatternVisual : MonoBehaviour
{
    private readonly int _fillAmountHash = Shader.PropertyToID("_Value");
    private Material _circlePatternMat;

    private void Awake()
    {
        _circlePatternMat = GetComponent<MeshRenderer>().material;
    }

    public void StartCirclePattern(Vector3 startPos, float radius, float duration, float afterDelay)
    {
        transform.position = startPos;
        transform.localScale = Vector3.one * radius;
        StartCoroutine(CirclePatternCoroutine(duration, afterDelay));
    }

    private IEnumerator CirclePatternCoroutine(float duration, float afterDelay)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * (1 / duration);
            _circlePatternMat.SetFloat(_fillAmountHash, EaseFunc.OutQuad(percent));
            yield return null;
        }

        yield return new WaitForSeconds(afterDelay);
        _circlePatternMat.SetFloat(_fillAmountHash, 0);
    }


   
}

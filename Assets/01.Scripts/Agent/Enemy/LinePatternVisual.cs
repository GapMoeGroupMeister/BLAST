using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePatternVisual : MonoBehaviour
{
    private readonly int _fillAmountHash = Shader.PropertyToID("_Value");
    private Material _linePatternMat;

    private void Awake()
    {
        _linePatternMat = GetComponent<MeshRenderer>().material;
    }

    public void StartLinePattern(Vector3 startPos, Vector3 endPos, float duration, float afterDelay, float xSize = 1)
    {
        Vector3 dir = endPos - startPos;
        transform.localScale = new Vector3(xSize, 1, dir.magnitude / 8f);
        transform.rotation = Quaternion.LookRotation(dir.normalized);
        Vector3 pos = Vector3.Lerp(startPos, endPos, 0.5f);
        pos.y = 0.05f;
        transform.position = pos;

        StartCoroutine(LinePatternCoroutine(duration, afterDelay));
    }

    private IEnumerator LinePatternCoroutine(float duration, float afterDelay)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * (1 / duration);
            _linePatternMat.SetFloat(_fillAmountHash, OutQuad(percent));
            yield return null;
        }

        yield return new WaitForSeconds(afterDelay);
        _linePatternMat.SetFloat(_fillAmountHash, 0);
    }


    private float OutQuad(float x)  
        => 1 - (1 - x) * (1 - x);
}

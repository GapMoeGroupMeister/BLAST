using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RedZoneManager : MonoBehaviour
{
    [SerializeField] private DecalProjector _redZoneDecal;
    [SerializeField] private float _redZoneDuration = 10f;
    [SerializeField] private WarningUI _warningUI;
    
    public void Start()
    {
        _redZoneDecal.gameObject.SetActive(false);
    }
    
    public void StartRedZone(Vector3 position, string warningMessage)
    {
        _redZoneDecal.transform.position = position;
        _redZoneDecal.gameObject.SetActive(true);
        _redZoneDecal.transform.DOScale(new Vector3(10,10, 10), 0.5f);
        _warningUI.OpenWarningPanel(warningMessage, 0.2f);
        StartCoroutine(EndRedZone());
    }

    private IEnumerator EndRedZone()
    {
        yield return new WaitForSeconds(2f);
        _warningUI.CloseWarningPanel(0.2f);
        yield return new WaitForSeconds(_redZoneDuration);
        _redZoneDecal.transform.DOScale(new Vector3(0,0, 10), 0.5f)
            .OnComplete(() => _redZoneDecal.gameObject.SetActive(false));
    }
}

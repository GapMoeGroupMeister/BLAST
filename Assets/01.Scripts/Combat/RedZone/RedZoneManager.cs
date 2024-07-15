using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RedZoneManager : MonoBehaviour
{
    [SerializeField] private RedZone _redZoneDecal;
    [SerializeField] private float _redZoneDuration = 10f;
    [SerializeField] private WarningUI _warningUI;
    
    public void Start()
    {
        _redZoneDecal.gameObject.SetActive(false);
        StartRedZone(Vector3.zero, "Warning! Red Zone is coming!");
    }
    
    public void StartRedZone(Vector3 position, string warningMessage)
    {
        _redZoneDecal.AreaCollider.enabled = false;
        _redZoneDecal.gameObject.SetActive(true);
        _redZoneDecal.RedZoneSet(position, 
                new Vector3(10, 10, 10), Ease.InSine, 
                () => _redZoneDecal.AreaCollider.enabled = true);
        _warningUI.OpenWarningPanel(warningMessage, 0.2f);
        StartCoroutine(EndRedZone());
    }

    private IEnumerator EndRedZone()
    {
        yield return new WaitForSeconds(2f);
        _warningUI.CloseWarningPanel(0.2f);
        yield return new WaitForSeconds(_redZoneDuration);
        _redZoneDecal.RedZoneSet(transform.position,
            new Vector3(0, 0, 10), Ease.OutSine,
            () => _redZoneDecal.gameObject.SetActive(false));
    }
}

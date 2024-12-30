using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserAligner : MonoBehaviour
{
    [SerializeField]
    private Enemy _owner;
    [SerializeField]
    private Vector3 _offset;
    private bool _isTurnOn;
    [SerializeField]
    private float _rotateSpeed = 120f;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private EnemyLaser[] _lasers;
    private float _activeTimer = 0;
    private float _activeTime = 0;

    private void Awake()
    {
        SetActiveLaser(false);
    }

    private void Update()
    {
        if (_isTurnOn)
        {
            _activeTimer += Time.deltaTime;
            if (_activeTimer > _activeTime)
            {
                SetActiveLaser(false);
                return;
            }
            transform.position = _owner.transform.position + _offset;
            transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
        }
    }

    public void SetActiveLaser(bool isActive, int count = 0, float duration = 8)
    {
        _isTurnOn = isActive;
        if(!isActive)
        {
            for(int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].gameObject.SetActive(false);
            }
            return;
        }
        _activeTime = duration;
        _activeTimer = 0;
        float angle = 360 / count;
        for (int i = 0; i < count; i++)
        {
            float curAngle = angle * i;
            float x = transform.position.x + Mathf.Sin(curAngle * Mathf.Deg2Rad) * _radius;
            float z = transform.position.z + Mathf.Cos(curAngle * Mathf.Deg2Rad) * _radius;
            _lasers[i].gameObject.SetActive(true);
            _lasers[i].transform.position = new Vector3(x, 15, z);
            Vector3 dir = _lasers[i].transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            Vector3 eulerAngle = Quaternion.LookRotation(dir).eulerAngles;
            eulerAngle.z = 90;
            eulerAngle.y += 90;
            _lasers[i].transform.rotation = Quaternion.Euler(eulerAngle);
            _lasers[i].EnableLaser(500);
        }
    }
}

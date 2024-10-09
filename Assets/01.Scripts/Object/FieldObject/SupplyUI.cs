using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyUI : MonoBehaviour
{
    private Transform _player, _supply;
    private RectTransform _rect;
    [SerializeField] private GameObject _image;
    [SerializeField] private float _offset;


    private void LateUpdate()
    {
        Vector3 dir = new Vector3(_player.position.x, 0, _player.position.z) - new Vector3(_supply.position.x, 0, _supply.position.z);

        float rotation = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + _offset;

        Vector3 position = Quaternion.Euler(0, 0, rotation) * Vector3.down;
        position *= ((Screen.height / 2) - 50);

        _rect.anchoredPosition = position;
        transform.eulerAngles = new Vector3(0, 0, rotation);

        if (dir.magnitude < Camera.main.orthographicSize + 3)
        {
            _image.SetActive(false);
        }
        else
        {
            _image.SetActive(true);
        }
    }

    public void Init(Transform supply)
    {
        _rect = transform as RectTransform;
        _player = GameManager.Instance.Player.transform;
        _supply = supply;
    }
}

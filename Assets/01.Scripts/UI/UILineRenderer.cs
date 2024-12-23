using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform _myTransform;
    [SerializeField] private Image _image;

    public void CreateLine(Vector3 from, Vector3 to, Color color, float width = 100)
    {
        from += new Vector3(Screen.width, Screen.height) / 2;
        to += new Vector3(Screen.width, Screen.height) / 2;

        _image.color = color;

        Vector2 point1 = new Vector2(to.x, to.y);
        Vector2 point2 = new Vector2(from.x, from.y);
        Vector2 midpoint = (point1 + point2) / 2f;

        _myTransform.position = midpoint;

        Vector2 dir = point1 - point2;
        _myTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        _myTransform.localScale = new Vector3(dir.magnitude / 100, width / 100, 1f);
    }
}

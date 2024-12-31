using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyScene.Direction
{

    public class LobbyFadeOutPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private Image _topImage;
        [SerializeField] private Image _bottomImage;
        [SerializeField] private float _duration;

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _topImage.DOFillAmount(1, _duration);
            _bottomImage.DOFillAmount(1, _duration);
            _topImage.transform.DORotate(new Vector3(0, 0, 360f), _duration);
            _bottomImage.transform.DORotate(new Vector3(0, 0, 360f), _duration);
        }
    }

}
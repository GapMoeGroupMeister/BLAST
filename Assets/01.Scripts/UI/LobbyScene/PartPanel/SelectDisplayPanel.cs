using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LobbyScene
{
    
    public class SelectDisplayPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private bool _isActive;

        [SerializeField] private float _activeDuration = 1f;

        [SerializeField] private QuadMeshDrawer _quadDrawer;

        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private TextMeshPro _descriptionText;

        public void ResetGraph()
        {
            _quadDrawer.DrawQuadGraph(new float[]{0, 0, 0, 0});
        }

        public void SelectPart(PlayerPartDataSO data)
        {
            _nameText.text = data.partName;
            float def = data.defence / 100f;
            float atk = data.damage / 100f;
            float utl = data.utility / 100f;
            float std = data.mobility / 100f;
            _quadDrawer.ShowQuadGraph(new float[]{def, atk, utl, std}, 1f);
        }

        public void Open()
        {
            transform.DOScaleY(1f, _activeDuration);
        }

        public void Close()
        {
            transform.DOScaleY(0f, _activeDuration);
        }
    }

}
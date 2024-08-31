using TMPro;
using UnityEngine;

public class SelectDisplayPanel : MonoBehaviour
{
    [SerializeField]
    private bool _isActive;

    [SerializeField] private QuadMeshDrawer _quadDrawer;

    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _descriptionText;

    public void SelectPart(PlayerPartDataSO data)
    {
        _nameText.text = data.partName;
        float def = data.defence / 100f;
        float atk = data.damage / 100f;
        float utl = data.utility / 100f;
        float std = data.mobility / 100f;
        _quadDrawer.DrawQuadGraph(new float[]{def, atk, utl, std});
    }

    
}

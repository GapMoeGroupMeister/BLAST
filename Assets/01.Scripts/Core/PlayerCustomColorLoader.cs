using System.Collections.Generic;
using EasySave.Json;
using LobbyScene.ColorSettings;
using UnityEngine;

public class PlayerCustomColorLoader : MonoBehaviour
{
    [SerializeField] private List<Renderer> _defaultRendererList;
    private static List<Renderer> _rendererList;
    private static readonly int Color1 = Shader.PropertyToID("_Color1");
    private static readonly int Color2 = Shader.PropertyToID("_Color2");
    private static readonly int Color3 = Shader.PropertyToID("_Color3");
    private static readonly int Light1 = Shader.PropertyToID("_Light");
    
    private static ColorSettingData _colorSettingData;
    
    private void Awake()
    {
        _rendererList = new List<Renderer>();
        _rendererList.AddRange(_defaultRendererList);
        var colorSetDataGroup = EasyToJson.FromJson<ColorSetDataGroup>("ColorSet");
        _colorSettingData = colorSetDataGroup.currnetData;
    }

    private static void AddRenderer(Renderer renderer)
    {
        _rendererList.Add(renderer);
        Color[] colors = LoadCustomColors();
        renderer.material.SetColor(Color1, colors[0]);
        renderer.material.SetColor(Color2, colors[1]);
        renderer.material.SetColor(Color3, colors[2]);
        renderer.material.SetColor(Light1, colors[3] + new Color(1.5f, 1.5f, 1.5f)); //HDR
    }

    public static void AddRenderers(Transform trm)
    {
        foreach (var renderer in trm.GetComponentsInChildren<Renderer>())
        {
            AddRenderer(renderer);
        }
    }
    
    private static Color[] LoadCustomColors()
    {
        Color[] colors = new Color[4];
        
        colors[0] = _colorSettingData.color1;
        colors[1] = _colorSettingData.color2;
        colors[2] = _colorSettingData.color3;
        colors[3] = _colorSettingData.lightColor;
        
        return colors;
    }
}

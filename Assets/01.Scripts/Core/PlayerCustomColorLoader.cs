using System;
using System.Collections.Generic;
using EasySave.Json;
using LobbyScene.ColorSettings;
using UnityEngine;

public class PlayerCustomColorLoader : MonoBehaviour
{
    private static List<Renderer> _rendererList;
    private static readonly int Color1 = Shader.PropertyToID("_Color1");
    private static readonly int Color2 = Shader.PropertyToID("_Color2");
    private static readonly int Color3 = Shader.PropertyToID("_Color3");
    private static readonly int Light1 = Shader.PropertyToID("_Light");
    
    private void Awake()
    {
        _rendererList = new List<Renderer>();
    }

    public static void AddRenderer(Renderer renderer)
    {
        _rendererList.Add(renderer);
        renderer.material.SetColor(Color1, Color.white);
        renderer.material.SetColor(Color2, Color.white);
        renderer.material.SetColor(Color3, Color.white);
        renderer.material.SetColor(Light1, Color.white);
    }

    private static Color[] LoadCustomColors()
    {
        Color[] colors = new Color[4];
        ColorSettingData colorSettingData = EasyToJson.FromJson<ColorSettingData>("ColorSet");
        
        colors[0] = colorSettingData.color1;
        colors[1] = colorSettingData.color2;
        colors[2] = colorSettingData.color3;
        colors[3] = colorSettingData.lightColor;
        
        return colors;
    }
}

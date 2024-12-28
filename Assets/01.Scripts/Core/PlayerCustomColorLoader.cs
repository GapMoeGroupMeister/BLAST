using System.Collections.Generic;
using System.Linq;
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
    
    private void Awake()
    {
        _rendererList = new List<Renderer>();

        foreach (Renderer renderer in _defaultRendererList)
        {
            AddRenderer(renderer);
        }
    }

    private static void AddRenderer(Renderer renderer)
    {
        _rendererList.Add(renderer);
        Color[] colors = LoadCustomColors();
        SetColor(renderer, colors);
    }
    public static void AddRenderers(Transform trm)
    {
        _rendererList = _rendererList.FindAll(x => x != null).ToList();

        foreach (var renderer in trm.GetComponentsInChildren<Renderer>())
        {
            AddRenderer(renderer);
        }
    }

    private static void SetColor(Renderer renderer, Color[] colors)
    {
        renderer.material.SetColor(Color1, colors[0]);
        renderer.material.SetColor(Color2, colors[1]);
        renderer.material.SetColor(Color3, colors[2]);
        renderer.material.SetColor(Light1, colors[3] + new Color(1.5f, 1.5f, 1.5f)); //HDR
    }

    public static void LoadAndSetColor(Color[] colors)
    {
        _rendererList = _rendererList.FindAll(x => x != null).ToList();
        foreach (Renderer renderer in _rendererList)
        {
            SetColor(renderer, colors);
        }
    }
    
    public static void LoadAndSetColor()
    {
        Color[] colors = LoadCustomColors();
        LoadAndSetColor(colors);
    }

    private static Color[] LoadCustomColors()
    {
        Color[] colors = EasyToJson.FromJson<ColorSetDataGroup>("ColorSet").currentData.colors;
        
        return colors;  
    } 
}

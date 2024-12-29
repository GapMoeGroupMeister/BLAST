using System.Collections.Generic;
using UnityEngine;
namespace LobbyScene.ColorSettings
{
    [System.Serializable]
    public class ColorSetDataGroup
    {
        public List<ColorSettingData> datas;

        public ColorSettingData currentData;

        public ColorSetDataGroup()
        {
            currentData = new ColorSettingData();
            currentData.colors = new[]
            {
                new Color(0.7725491f, 0.764706f, 0.7764707f),
                new Color(0.2745097f, 0.2862744f, 0.2980391f),
                new Color(0.2980391f, 0.3607842f, 0.4078431f),
                new Color(0.7490196f, 0.7490196f, 0.7490196f) 
            };
            datas = new List<ColorSettingData>();
        }
    }

    [System.Serializable]
    public class ColorSettingData
    {
        
        public string colorSetName;


        public Color[] colors;

        public ColorSettingData()
        {
            colors = new Color[4];
        }
    
        public ColorSettingData(ColorSettingData originData)
        {
            colorSetName = originData.colorSetName;
            colors = new Color[4];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = originData.colors[i];
            }
        }
    }
}
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
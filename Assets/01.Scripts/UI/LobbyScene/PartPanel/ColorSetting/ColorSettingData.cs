using System.Collections.Generic;
using UnityEngine;
namespace LobbyScene.ColorSettings
{
    [System.Serializable]
    public class ColorSetDataGroup
    {
        public List<ColorSettingData> datas;

        public ColorSettingData currnetData;
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
            colors = originData.colors;
        }
    }
}
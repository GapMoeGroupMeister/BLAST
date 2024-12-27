using System.Collections.Generic;
using UnityEngine;
namespace LobbyScene.ColorSettings
{
    public class ColorSetDataGroup
    {
        public List<ColorSettingData> datas;

        public ColorSettingData currnetData;
    }
    
    [System.Serializable]
    public class ColorSettingData
    {
        
        public string colorSetName;

        public Color color1;
        public Color color2;
        public Color color3;
        public Color lightColor;
    
    
        public ColorSettingData(ColorSettingData originData)
        {
            colorSetName = originData.colorSetName;
            color1 = originData.color1;
            color2 = originData.color2;
            color3 = originData.color3;
            lightColor = originData.lightColor;
        }
    }
}
using EasySave.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : UIPanel
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private TMP_Dropdown _screenSettingDropDown;

    private readonly string dataPath = "GameSetting";
    
    private SettingData _currentSetting;

    protected override void Awake()
    {
        base.Awake();
        _bgmSlider.onValueChanged.AddListener(HandleBGMChanged);
        _sfxSlider.onValueChanged.AddListener(HandleSFXChanged);
        _screenSettingDropDown.onValueChanged.AddListener(HandleScreenSizeChanged);
        
        LoadSetting();
    }

   

    private void HandleBGMChanged(float value)
    {
        _currentSetting.bgmVolume = (int)value;
        _audioMixer.SetFloat("Volume_BGM", value);
    }
    
    private void HandleSFXChanged(float value)
    {
        _currentSetting.sfxVolume = (int)value;
        if (value <= -40f)
            value = -80f;
        _audioMixer.SetFloat("Volume_SFX", value);
        
        
    }
    
    private void HandleScreenSizeChanged(int selectIndex)
    {
        _currentSetting.screenSizeIndex = selectIndex; 
        switch (selectIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(1240, 860, FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);

                break;
        }
    }
    
    private void LoadSetting()
    {
        _currentSetting = EasyToJson.FromJson<SettingData>(dataPath);
        if (_currentSetting == null)
            _currentSetting = new SettingData();
        HandleBGMChanged(_currentSetting.bgmVolume);
        HandleSFXChanged(_currentSetting.sfxVolume);
        HandleScreenSizeChanged(_currentSetting.screenSizeIndex);
    }
    
    public void SaveSetting()
    {
        EasyToJson.ToJson<SettingData>(_currentSetting, dataPath);        
    }
}
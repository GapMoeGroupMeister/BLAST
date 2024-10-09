using System;
using System.Collections.Generic;
using Crogen.PowerfulInput;
using UnityEngine;

public enum InGameUIEnum
{
    Pause,
    Setting,
    Status,
    EffectSelect,
    Fade, 
    BossWarning
}

public class UIManager : MonoSingleton<UIManager>
{
    public UIInputReader inputReader;
    [SerializeField] private InGameUIInfo[] _UIs;
    [SerializeField] private Transform _canvasTrm;
    private Dictionary<InGameUIEnum, IWindowPanel> uiPanelsDictionary = new Dictionary<InGameUIEnum, IWindowPanel>();
    private Transform _gameCanvasTrm;
    private Transform _eventCanvasTrm;
    private Transform _systemCanvasTrm;
    
    protected override void Awake()
    {
        base.Awake();
        _gameCanvasTrm = _canvasTrm.Find("GameCanvas");
        _eventCanvasTrm = _canvasTrm.Find("EventCanvas");
        _systemCanvasTrm = _canvasTrm.Find("SystemCanvas");
        
        Initialize();

        inputReader.OnEscEvent += HandleOpenPause;

    }

   

    private void Initialize()
    {
        foreach (InGameUIInfo uiInfo in _UIs)
        {
            Transform targetCanvas =null;
            switch (uiInfo.canvasType)
            {
                case UICanvasType.GameUI:
                    targetCanvas = _gameCanvasTrm;
                    break;
                case UICanvasType.SystemUI:
                    targetCanvas = _systemCanvasTrm;
                    break;
                case UICanvasType.EventUI:
                    targetCanvas = _eventCanvasTrm;
                    break;
            }
            IWindowPanel window = targetCanvas.Find($"{uiInfo.uiType.ToString()}Panel").GetComponent<IWindowPanel>();
            uiPanelsDictionary.Add(uiInfo.uiType, window);
        }
    }

    public void Open(InGameUIEnum type)
    {
        uiPanelsDictionary[type].Open();
    }

    public void Close(InGameUIEnum type)
    {
        uiPanelsDictionary[type].Close();
    }
    
    private void HandleOpenPause()
    {
        Open(InGameUIEnum.Pause);
        Open(InGameUIEnum.Fade);
    }
}
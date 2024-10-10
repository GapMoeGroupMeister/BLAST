using System;
using System.Collections.Generic;
using Crogen.PowerfulInput;
using UnityEngine;
using UnityEngine.Serialization;

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
    public Transform canvasTrm;
    private Dictionary<InGameUIEnum, IWindowPanel> uiPanelsDictionary = new Dictionary<InGameUIEnum, IWindowPanel>();
    private Transform _gameCanvasTrm;
    private Transform _eventCanvasTrm;
    private Transform _systemCanvasTrm;
    
    protected override void Awake()
    {
        base.Awake();
        uiPanelsDictionary.Clear();
        _gameCanvasTrm = canvasTrm.Find("GameCanvas");
        _eventCanvasTrm = canvasTrm.Find("EventCanvas");
        _systemCanvasTrm = canvasTrm.Find("SystemCanvas");
        
        Initialize();

        inputReader.OnEscEvent += HandleOpenPause;

    }

    private void OnDisable()
    {
        inputReader.OnEscEvent -= HandleOpenPause;
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
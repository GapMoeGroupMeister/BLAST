using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StageDifficulty
{
    Easy = 1,
    Normal,
    Hard,
    Impossible
}

public class StageSelectPanel : MonoBehaviour
{
    [SerializeField] private StageSelectSlot _slotPrefab;
    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private TextMeshProUGUI _stageDescriptionText;
    [SerializeField] private TextMeshProUGUI _stageDifficultyText;
    private readonly string _baseDifficultyText = "<color=white>위험도 </color>";

    [Header("Setting")]
    [SerializeField] private Color _difficultyStartColor;
    [SerializeField] private Color _difficultyEndColor;


    public void SelectStage(StageInfoSO stageInfo)
    {
        _stageNameText.text = stageInfo.stageName;
        _stageDescriptionText.text = stageInfo.stageDescription;
        _stageDifficultyText.text = $"{_baseDifficultyText}{stageInfo.stageDifficulty.ToString()}";

        Color difficultyColor = Color.Lerp(_difficultyStartColor, _difficultyEndColor, (int)stageInfo.stageDifficulty * 0.25f);
        _stageDifficultyText.color = difficultyColor;

        
    }
}

using TMPro;
using UnityEngine;

public class PlayTimePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Update()
    {
        _timeText.text = TimeManager.CurrentGlobalTimerString;
    }
}

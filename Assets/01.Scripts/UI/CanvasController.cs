using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
namespace UI
{

    public class CanvasController : MonoBehaviour, IWindowPanel
    {


        private CanvasGroup _canvasGroup;
        [SerializeField]
        private bool _isActive = true;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                if (_isActive)
                    Close();
                else
                    Open();
                _isActive = !_isActive;
            }
        }
        public void Close()
        {

            _canvasGroup.alpha = 0f;
        }

        public void Open()
        {
            _canvasGroup.alpha = 1f;
        }
    }

}
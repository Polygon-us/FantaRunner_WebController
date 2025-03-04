using UnityEngine;

namespace UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ControllerBase registerPanel;
        [SerializeField] private ControllerBase swipePanel;
        [SerializeField] private ControllerBase gameOverPanel;

        private ControllerBase currentMenu;

      
        private void Awake()
        {
            registerPanel.gameObject.SetActive(false);
            swipePanel.gameObject.SetActive(false);
            gameOverPanel.gameObject.SetActive(false);

            registerPanel.OnCreation(this);
            swipePanel.OnCreation(this);
            gameOverPanel.OnCreation(this);
            
            ShowRegister();
        }

        private void ShowPanel(ControllerBase panel)
        {
            currentMenu?.gameObject.SetActive(false);
            currentMenu?.OnHide();
            currentMenu = panel;
            currentMenu.gameObject.SetActive(true);
            currentMenu.OnShow();
        }

        public void ShowRegister()
        {
            ShowPanel(registerPanel);
        }

        public void ShowDirection()
        {
            ShowPanel(swipePanel);
        }

        public void ShowGameOver()
        {
            ShowPanel(gameOverPanel);
        }
    }
}
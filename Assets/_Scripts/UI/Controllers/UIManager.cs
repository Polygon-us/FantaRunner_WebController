using UnityEngine;

namespace UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RegisterController registerPanel;
        [SerializeField] private SwipeManager swipePanel;
        // [SerializeField] private GameOverController gameOverPanel;

        private GameObject currentMenu;

        private void OnEnable()
        {
            registerPanel.OnRegistered += ShowDirection;
        }

        private void OnDisable()
        {
            registerPanel.OnRegistered -= ShowDirection;
        }

        private void Awake()
        {
            registerPanel.gameObject.SetActive(false);
            swipePanel.gameObject.SetActive(false);
            // gameOverPanel.gameObject.SetActive(false);

            ShowRegister();
        }

        private void ShowPanel(GameObject panel)
        {
            currentMenu?.SetActive(false);
            currentMenu = panel;
            currentMenu.SetActive(true);
        }

        private void ShowRegister()
        {
            ShowPanel(registerPanel.gameObject);
        }

        public void ShowDirection()
        {
            ShowPanel(swipePanel.gameObject);
        }

        public void ShowGameOver()
        {
            // ShowPanel(gameOverPanel.gameObject);
        }
    }
}
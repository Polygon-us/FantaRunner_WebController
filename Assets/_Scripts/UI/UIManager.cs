using UnityEngine;

namespace UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RegisterController registerPanel;
        [SerializeField] private SwipeManager swipePanel;

        private GameObject currentMenu;

        private void OnEnable()
        {
            registerPanel.OnRegistered += ShowDirection;
        }

        private void OnDisable()
        {
            registerPanel.OnRegistered -= ShowDirection;
        }

        private void Start()
        {
            registerPanel.gameObject.SetActive(false);
            swipePanel.gameObject.SetActive(false);

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

        private void ShowDirection()
        {
            ShowPanel(swipePanel.gameObject);
        }
    }
}
using FirebaseCore.Listeners;
using FirebaseCore.Senders;
using FirebaseCore.DTOs;
using UnityEngine;

namespace UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ControllerBase registerPanel;
        [SerializeField] private ControllerBase swipePanel;
        [SerializeField] private ControllerBase gameOverPanel;
        [SerializeField] private RoomConfig roomConfig;


        private ControllerBase currentMenu;

        private GameStateSender gameStateSender;
        private GameStateListener gameStateListener;

        private void Awake()
        {
            registerPanel.gameObject.SetActive(false);
            swipePanel.gameObject.SetActive(false);
            gameOverPanel.gameObject.SetActive(false);

            registerPanel.OnCreation(roomConfig);
            swipePanel.OnCreation(roomConfig);
            gameOverPanel.OnCreation(roomConfig);
        }

        private void Start()
        {
            gameStateSender = new GameStateSender(roomConfig.roomName);
            gameStateListener = new GameStateListener(roomConfig.roomName);

            gameStateListener.OnDataReceived += OnStateChanged;

            GameStateDto gameStateDto = new GameStateDto
            {
                state = GameStates.Register
            };
            
            gameStateSender.Send(gameStateDto);
        }

        private void ShowPanel(ControllerBase panel)
        {
            currentMenu?.gameObject.SetActive(false);
            currentMenu?.OnHide();
            currentMenu = panel;
            currentMenu.gameObject.SetActive(true);
            currentMenu.OnShow();
        }

        private void ShowRegister()
        {
            ShowPanel(registerPanel);
        }

        private void ShowDirection()
        {
            ShowPanel(swipePanel);
        }

        private void ShowGameOver()
        {
            ShowPanel(gameOverPanel);
        }

        private void OnStateChanged(GameStateDto state)
        {
            switch (state.state)
            {
                case GameStates.Register:
                    ShowRegister();
                    break;
                case GameStates.Game:
                    ShowDirection();
                    break;
                case GameStates.End:
                    ShowGameOver();
                    break;
            }
        }

        private void OnDisable()
        {
            gameStateListener.Disconnect();
        }
    }
}
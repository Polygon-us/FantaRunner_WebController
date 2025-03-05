using FirebaseCore.DTOs;
using FirebaseCore.Senders;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.Controllers
{
    public class GameOverController : ControllerBase
    {
        [SerializeField] private Button resetBtn;
        [SerializeField] private TMP_Text countdownTxt;
        [SerializeField] private int countdownTime;

        private int tweenId;
        
        GameStateSender gameStateSender;
        UserSender userSender;
        GameStateDto gameStateDto;

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            userSender = new UserSender(roomConfig.roomName);
            gameStateSender = new GameStateSender(roomConfig.roomName);
            
            resetBtn.onClick.AddListener(OnReset);
        }

        public override void OnShow()
        {
            tweenId = LeanTween.value(countdownTime, 0, countdownTime).setOnUpdate(val =>
                countdownTxt.text = Mathf.CeilToInt(val).ToString()).setOnComplete(OnCountDown).uniqueId;
        }

        public override void OnHide()
        {
        }

        private void OnCountDown()
        {
            userSender.Delete();
            
            gameStateDto.state = GameStates.Register;
            gameStateSender.Send(gameStateDto);
        }
        
        private void OnReset()
        {
            LeanTween.cancel(tweenId);

            gameStateDto.state = GameStates.Game;
            gameStateSender.Send(gameStateDto);
        }

    }
}
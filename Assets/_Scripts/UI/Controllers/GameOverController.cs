using FirebaseCore.Senders;
using FirebaseCore.DTOs;
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
        GameStateDto gameStateDto;

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            gameStateSender = new GameStateSender(roomConfig.roomName);
            
            resetBtn.onClick.AddListener(OnReset);
        }

        public override void OnShow()
        {
            tweenId = LeanTween.value(countdownTime, 0, countdownTime).setOnUpdate(
                value => countdownTxt.text = Mathf.CeilToInt(value).ToString()).id;
        }

        public override void OnHide()
        {
        }

        private void OnReset()
        {
            LeanTween.cancel(tweenId);

            gameStateDto.state = GameStates.Game;
            gameStateSender.Send(gameStateDto);
        }

    }
}
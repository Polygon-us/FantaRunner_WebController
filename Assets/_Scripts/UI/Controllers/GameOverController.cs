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

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            resetBtn.onClick.AddListener(OnReset);
        }

        public override void OnShow()
        {
            tweenId = LeanTween.value(countdownTime, 0, countdownTime).setOnUpdate(val =>
                countdownTxt.text = Mathf.CeilToInt(val).ToString()).uniqueId;
        }

        public override void OnHide()
        {
        }

        private void OnReset()
        {
            LeanTween.cancel(tweenId);
        }

    }
}
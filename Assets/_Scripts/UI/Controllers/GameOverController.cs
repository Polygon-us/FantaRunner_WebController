using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button resetBtn;
    [SerializeField] private TMP_Text countdownTxt;
    [SerializeField] private int countdownTime;
    private int tweenId;
    
    private void Awake()
    {
        resetBtn.onClick.AddListener(OnReset);
    }

    private void Start()
    {
        tweenId = LeanTween.value(countdownTime, 0, countdownTime).setOnUpdate(val =>
            countdownTxt.text = Mathf.CeilToInt(val).ToString()).uniqueId;
    }

    private void OnReset()
    {
        LeanTween.cancel(tweenId);
    }
}

using TMPro;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private RectTransform blocker;
    [SerializeField] private RectTransform container;
    
    public void Show(string message)
    {
        gameObject.SetActive(true);
        messageText.SetText(message);
        
        container.localScale = Vector3.zero;
        
        LeanTween.scale(container, Vector3.one, 0.2f).setEaseInCubic();
        LeanTween.alpha(blocker, 0.5f, 0.2f);

        LeanTween.delayedCall(3, Close);
    }

    private void Close()
    {
        LeanTween.scale(container, Vector3.zero, 0.2f).setEaseInCubic();
        LeanTween.alpha(blocker, 0.0f, 0.2f).setOnComplete(
           () => gameObject.SetActive(false));
    }
}

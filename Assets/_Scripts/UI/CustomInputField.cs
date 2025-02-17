using UnityEngine;
using TMPro;

public class CustomInputField : MonoBehaviour
{
    [SerializeField] private string labelText;
    [SerializeField] private string placeHolderText;
    [SerializeField] private int textsSize;
    [SerializeField] private TMP_InputField.ContentType contentType;
    [SerializeField] private TouchScreenKeyboardType keyboardType;
    
    private TMP_Text label;
    private TMP_InputField inputField;

    public string Text => inputField.text;
    
    private void EnsureInitialized()
    {
        label ??= GetComponentInChildren<TMP_Text>();
        inputField ??= GetComponentInChildren<TMP_InputField>();
    }

    private void OnValidate()
    {
        EnsureInitialized();
        
        label.text = labelText;
        ((TMP_Text)inputField.placeholder).text = placeHolderText;
        inputField.contentType = contentType;
    }

    private void Awake()
    {
        EnsureInitialized();
        
        inputField.onSelect.AddListener(ShowKeyboard);
    }

    private void ShowKeyboard(string text)
    {
        print("Show Keyboard");
        TouchScreenKeyboard.Open(text, keyboardType, false, false);
    }

    private void OnDestroy()
    {
        inputField.onSelect.RemoveAllListeners();
    }
}

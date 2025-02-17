using UnityEngine;
using TMPro;

public class CustomInputField : MonoBehaviour
{
    [SerializeField] private string labelText;
    [SerializeField] private string placeHolderText;
    [SerializeField] private int textsSize;

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
    }
}

using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        text.text = $"version: {Application.version}";
    }
}
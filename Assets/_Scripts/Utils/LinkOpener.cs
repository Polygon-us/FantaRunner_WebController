using UnityEngine.UI;
using UnityEngine;

namespace Utils
{
    public class LinkOpener : MonoBehaviour
    {
        [SerializeField] private string url;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenLink);
        }

        private void OpenLink()
        {
            Application.OpenURL(url);
        }
    }
}
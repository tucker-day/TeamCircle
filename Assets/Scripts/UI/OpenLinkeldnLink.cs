using UnityEngine;
using UnityEngine.UI;

public class OpenLinkeldnLink : MonoBehaviour
{
    [SerializeField] public string url;

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}

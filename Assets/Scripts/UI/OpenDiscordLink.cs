using UnityEngine;
using UnityEngine.UI;

public class OpenDiscordLink : MonoBehaviour
{
    [SerializeField] public string url;

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}

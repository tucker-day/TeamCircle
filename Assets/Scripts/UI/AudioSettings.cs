using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider soundSlider;


    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { SaveMusicChanges(); });
        soundSlider.onValueChanged.AddListener(delegate { SaveSoundChanges(); });

        if (PlayerPrefs.HasKey("musicVol"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVol");
        }
        if (PlayerPrefs.HasKey("soundVol"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("soundVol");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveMusicChanges()
    {
        PlayerPrefs.SetFloat("musicVol", musicSlider.value);
        PlayerPrefs.Save();

        Debug.Log("Music volume set to: " + musicSlider.value);
    }
    public void SaveSoundChanges()
    {
        PlayerPrefs.SetFloat("soundVol", soundSlider.value);
        PlayerPrefs.Save();

        Debug.Log("Sound volume set to: " + soundSlider.value);
    }
}

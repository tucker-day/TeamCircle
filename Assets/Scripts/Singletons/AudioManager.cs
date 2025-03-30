using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    AudioSource mus_calm, mus_combat, mus_miniboss, sfxAudio;

    [SerializeField]
    AudioMixer masterMixer;

    public AudioClip[] soundEffects;

    bool combatToCalm;
    bool minibossToCalm;

    bool calmToCombat;
    bool minibossToCombat;

    bool calmToMiniboss;
    bool combatToMiniboss;

    public float musVolume;
    public float sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize an instance of the audio manager.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        combatToCalm = true;

        CheckMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance)
        {
            CheckMusicUpdate();
        }

        CheckMusicVolume();
        TestSoundEffects();
    }

    // This function checks for any volume changes and adjusts volume based on said changes.
    void CheckMusicVolume()
    {
        if (musVolume != PlayerPrefs.GetFloat("musicVol"))
        {
            musVolume = PlayerPrefs.GetFloat("musicVol");
            masterMixer.SetFloat("musVol", Mathf.Log10(Mathf.Clamp(musVolume, 0.0001f, 1f)) * 80 / 4f);
            Debug.Log("Music volume: " + musVolume);
        }
        if (sfxVolume != PlayerPrefs.GetFloat("soundVol"))
        {
            sfxVolume = PlayerPrefs.GetFloat("soundVol");
            masterMixer.SetFloat("sfxVol", Mathf.Log10(Mathf.Clamp(sfxVolume, 0.0001f, 1f)) * 80 / 4f);
            Debug.Log("Sound volume: " + sfxVolume);
        }
    }

    void CheckMusicUpdate()
    {
        // For testing fading between music variants.
        // Press Escape to switch to Calm music.
        // Press Tab to switch to Combat music.
        // Press Left Shift to switch to Miniboss music.

        if ((Input.GetKeyDown(KeyCode.Escape) && mus_calm.volume == 0f) || !GameManager.instance.CheckForEnemies())
        {
            combatToCalm = true;
            minibossToCalm = true;

            calmToCombat = false;
            minibossToCombat = false;
            calmToMiniboss = false;
            combatToMiniboss = false;

            StartCoroutine("FadeMusic");
        }
        if ((Input.GetKeyDown(KeyCode.Tab) && mus_combat.volume == 0f) || GameManager.instance.CheckForEnemies() && !GameManager.instance.minibossPresent)
        {
            calmToCombat = true;
            minibossToCombat = true;

            combatToCalm = false;
            minibossToCalm = false;
            calmToMiniboss = false;
            combatToMiniboss = false;

            StartCoroutine("FadeMusic");
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) && mus_miniboss.volume == 0f) || GameManager.instance.minibossPresent)
        {
            calmToMiniboss = true;
            combatToMiniboss = true;

            calmToCombat = false;
            minibossToCombat = false;
            combatToCalm = false;
            minibossToCalm = false;

            StartCoroutine("FadeMusic");
        }
    }

    // This function will play a sound effect with a slight random pitch variation.
    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.pitch = Random.Range(0.9f, 1.1f);
        sfxAudio.PlayOneShot(clip);
    }

    // This function will stop all music from playing.
    public void StopMusic()
    {
        mus_calm.Stop(); mus_combat.Stop(); mus_miniboss.Stop();
    }

    // This function is for testing sound effects.
    // Press any of the keys below to play a sound.
    void TestSoundEffects()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Press 1 to play melee enemy attack sound.
            PlaySFX(soundEffects[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Press 2 to play melee enemy damage sound.
            PlaySFX(soundEffects[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Press 3 to play melee enemy death sound.
            PlaySFX(soundEffects[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Press 4 to play ranged enemy attack sound.
            PlaySFX(soundEffects[3]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // Press 5 to play ranged enemy damage sound.
            PlaySFX(soundEffects[4]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // Press 6 to play ranged enemy death sound.
            PlaySFX(soundEffects[5]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // Press 7 to play axe attack sound.
            PlaySFX(soundEffects[8]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // Press 8 to play spear attack sound.
            PlaySFX(soundEffects[9]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // Press 9 to play sword attack sound.
            PlaySFX(soundEffects[10]);
        }
    }

    // Coroutine for fading music.
    IEnumerator FadeMusic()
    {
        if (mus_calm.volume < 0)
        {
            mus_calm.volume = 0;
        }
        if (mus_combat.volume < 0)
        {
            mus_combat.volume = 0;
        }
        if (mus_miniboss.volume < 0)
        {
            mus_miniboss.volume = 0;
        }

        // If combat or miniboss to calm is true, fade into calm version.
        if (combatToCalm && mus_combat.volume > 0.00f || minibossToCalm && mus_miniboss.volume > 0.00f)
        {
            mus_calm.volume += 0.002f;
            mus_combat.volume -= 0.002f;
            mus_miniboss.volume -= 0.002f;
            yield return null;
        }

        // If calm or miniboss to combat is true, fade into combat version.
        else if (calmToCombat && mus_calm.volume > 0.00f || minibossToCombat && mus_miniboss.volume > 0.00f)
        {
            mus_combat.volume += 0.002f;
            mus_calm.volume -= 0.002f;
            mus_miniboss.volume -= 0.002f;
            yield return null;
        }

        // If calm or combat to miniboss is true, fade into miniboss version.
        else if (calmToMiniboss && mus_calm.volume > 0.00f || combatToMiniboss && mus_combat.volume > 0.00f)
        {
            mus_miniboss.volume += 0.002f;
            mus_calm.volume -= 0.002f;
            mus_combat.volume -= 0.002f;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameManager gameState;

    [SerializeField]
    AudioSource mus_calm, mus_combat, sfxAudio;

    public AudioClip[] soundEffects;

    bool combatToCalm;
    bool calmToCombat;

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

        combatToCalm = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMusicUpdate();
        MusicFade();
        TestSoundEffects();
    }

    void CheckMusicUpdate()
    {
        // For testing fading between music variants.
        // Press Escape to switch to Combat music.
        // Press Tab to switch to Calm music.
        if ((Input.GetKeyDown(KeyCode.Escape) && mus_combat.volume == 0f) || gameState.enemiesPresent)
        {
            calmToCombat = true;
            combatToCalm = false;
            gameState.enemiesPresent = true;
        }
        if ((Input.GetKeyDown(KeyCode.Tab) && mus_calm.volume == 0f) || !gameState.enemiesPresent)
        {
            combatToCalm = true;
            calmToCombat = false;
            gameState.enemiesPresent = false;
        }
    }

    void MusicFade()
    {
        // If combat to calm is true, fade from combat into calm version.
        if (combatToCalm && mus_combat.volume > 0.00f)
        {
            mus_calm.volume += 0.002f;
            mus_combat.volume -= 0.002f;
        }

        // If calm to combat is true, fade from calm into combat version.
        if (calmToCombat && mus_calm.volume > 0.00f)
        {
            mus_combat.volume += 0.002f;
            mus_calm.volume -= 0.002f;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    // This function is for testing sound effects.
    // Press any of the keys below to play a sound.
    void TestSoundEffects()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Press Q to play melee enemy attack sound.
            PlaySFX(soundEffects[0]);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // Press W to play melee enemy damage sound.
            PlaySFX(soundEffects[1]);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Press E to play melee enemy death sound.
            PlaySFX(soundEffects[2]);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Press A to play ranged enemy attack sound.
            PlaySFX(soundEffects[3]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Press S to play ranged enemy damage sound.
            PlaySFX(soundEffects[4]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Press D to play ranged enemy death sound.
            PlaySFX(soundEffects[5]);
        }
    }
}

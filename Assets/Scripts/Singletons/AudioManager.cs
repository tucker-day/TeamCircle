using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameManager gameState;

    [SerializeField]
    AudioSource mus_calm, mus_combat, mus_miniboss, sfxAudio;

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

        combatToCalm = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMusicUpdate();
        TestSoundEffects();
    }

    void CheckMusicUpdate()
    {
        // For testing fading between music variants.
        // Press Escape to switch to Calm music.
        // Press Tab to switch to Combat music.
        // Press Left Shift to switch to Miniboss music.

        if ((Input.GetKeyDown(KeyCode.Escape) && mus_calm.volume == 0f) || !gameState.enemiesPresent)
        {
            combatToCalm = true;
            minibossToCalm = true;

            calmToCombat = false;
            minibossToCombat = false;
            calmToMiniboss = false;
            combatToMiniboss = false;

            gameState.enemiesPresent = false;
            gameState.minibossPresent = false;
            StartCoroutine("FadeMusic");
        }
        if ((Input.GetKeyDown(KeyCode.Tab) && mus_combat.volume == 0f) || gameState.enemiesPresent && !gameState.minibossPresent)
        {
            calmToCombat = true;
            minibossToCombat = true;

            combatToCalm = false;
            minibossToCalm = false;
            calmToMiniboss = false;
            combatToMiniboss = false;

            gameState.enemiesPresent = true;
            gameState.minibossPresent = false;
            StartCoroutine("FadeMusic");
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) && mus_miniboss.volume == 0f) || gameState.minibossPresent)
        {
            calmToMiniboss = true;
            combatToMiniboss = true;

            calmToCombat = false;
            minibossToCombat = false;
            combatToCalm = false;
            minibossToCalm = false;

            gameState.enemiesPresent = true;
            gameState.minibossPresent = true;
            StartCoroutine("FadeMusic");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.pitch = Random.Range(0.9f, 1.1f);
        sfxAudio.PlayOneShot(clip);
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
            PlaySFX(soundEffects[6]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // Press 8 to play spear attack sound.
            PlaySFX(soundEffects[7]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // Press 9 to play sword attack sound.
            PlaySFX(soundEffects[8]);
        }
    }

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

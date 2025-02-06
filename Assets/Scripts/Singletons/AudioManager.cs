using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameManager gameState;

    [SerializeField]
    AudioSource mus_calm, mus_combat;

    AudioSource sfxAudio;

    bool combatToCalm = false;
    bool calmToCombat = false;

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

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                sfxAudio = source;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckMusicUpdate();
        MusicFade();
    }

    void CheckMusicUpdate()
    {
        // For testing fading between music variants.
        // Press Escape to switch to Combat music.
        // Press Tab to switch to Calm music.
        if ((Input.GetKey(KeyCode.Escape) && mus_combat.volume == 0f) || gameState.enemiesPresent)
        {
            calmToCombat = true;
            combatToCalm = false;
            gameState.enemiesPresent = true;
        }
        if ((Input.GetKey(KeyCode.Tab) && mus_calm.volume == 0f) || !gameState.enemiesPresent)
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
}

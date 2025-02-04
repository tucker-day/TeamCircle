using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    AudioSource mus_calm, mus_combat;
    float calmVol = 1f;
    float combatVol = 0f;

    bool combatToCalm = false;

    bool calmToCombat = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && mus_combat.volume == 0f)
        {
            calmToCombat = true;
            combatToCalm = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && mus_calm.volume == 0f)
        {
            combatToCalm = true;
            calmToCombat = false;
        }

        if (combatToCalm && mus_combat.volume > 0.00f)
        {
            mus_calm.volume += 0.002f;
            mus_combat.volume -= 0.002f;
        }

        if (calmToCombat && mus_calm.volume > 0.00f)
        {
            mus_combat.volume += 0.002f;
            mus_calm.volume -= 0.002f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }

        PlaySound("MainTheme");
        PlaySound("Button");
    }

    public void PlaySound(string soundName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == soundName)
                s.source.Play();
        }
    }
}

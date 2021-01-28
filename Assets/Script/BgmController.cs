using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute;

    [SerializeField]
    private float
        delay = 0.1f;
    [SerializeField]
    private AudioClip
        intro;
    [SerializeField]
    private AudioClip
        loop;

    AudioSource BGM_intro;
    AudioSource BGM_loop;

    void Start()
    {
        BGM_intro = gameObject.AddComponent<AudioSource>();
        BGM_loop = gameObject.AddComponent<AudioSource>();


        if (intro != null)
        {
            BGM_intro.playOnAwake = false;
            BGM_intro.clip = intro;
            BGM_intro.loop = false;
            BGM_intro.PlayScheduled(AudioSettings.dspTime + delay);     //isplaying化

            if (loop != null)
            {
                BGM_loop.playOnAwake = false;
                BGM_loop.clip = loop;
                BGM_loop.loop = true;
                BGM_loop.PlayScheduled(AudioSettings.dspTime + delay + intro.length);
            }
        }
    }
    void Update()
    {
        BGM_intro.mute = mute;
        BGM_loop.mute = mute;
        BGM_intro.volume = vol;
        BGM_loop.volume = vol;
    }
}
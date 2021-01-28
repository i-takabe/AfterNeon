using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1ゲーム内で何回も鳴るSE用スクリプト

public class SEController : MonoBehaviour
{
    Animator Anm;
    private bool[] seBool = new bool[4];

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    public AudioClip Jump_SE;
    public AudioClip Slide_SE;
    public AudioClip Run_SE;
    public AudioClip Dmg_SE;


    AudioSource SoundEffecter;

    void Start()
    { 

        SoundEffecter = gameObject.AddComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SoundEffecter.PlayOneShot(Jump_SE);

        if (Input.GetKeyDown(KeyCode.LeftControl))
            SoundEffecter.PlayOneShot(Slide_SE);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            SoundEffecter.PlayOneShot(Run_SE);

        SoundEffecter.mute = mute;
        SoundEffecter.volume = vol;
    }

    public void DamageSE()
    {
        SoundEffecter.PlayOneShot(Run_SE);
        SoundEffecter.mute = mute;
        SoundEffecter.volume = vol;
    }

}

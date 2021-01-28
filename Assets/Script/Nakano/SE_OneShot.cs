using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1ゲーム内で1回しかならないSE用スクリプト

public class SE_OneShot : MonoBehaviour
{
    private bool[] seBool = new bool[4];

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    public AudioClip Goal_SE;

    [SerializeField, Header("CountdownSE")]
    AudioClip[] Count_se = new AudioClip[4];

    AudioSource SoundEffecter;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            seBool[i] = true;
        }

        SoundEffecter = gameObject.AddComponent<AudioSource>();

    }

    public void Count_SE(int i)
    {
        if (seBool[i])
        {
            SoundEffecter.PlayOneShot(Count_se[i]);
            SoundEffecter.mute = mute;
            SoundEffecter.volume = vol;
            seBool[i] = false;
        }
    }

    public void Goal()
    {
        SoundEffecter.PlayOneShot(Goal_SE);
        SoundEffecter.mute = mute;
        SoundEffecter.volume = vol;
    }
}

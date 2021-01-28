using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soundの鳴らす鳴らさないを管理

public class SoundCtrl : MonoBehaviour
{

    public void SetBGM(int i)
    {
        if (i == 0)
            this.gameObject.GetComponent<BgmController>().enabled = true;

        if(i == 1)
            this.gameObject.GetComponent<BgmController>().mute = true;
    }

    public void SetActionSE(int i)
    {
        if (i == 0)
            this.gameObject.GetComponent<SEController>().mute = false;

        if (i == 1)
            this.gameObject.GetComponent<SEController>().mute = true;
    }
}

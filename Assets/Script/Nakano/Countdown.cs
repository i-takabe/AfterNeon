using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Sprite image_3;
    public Sprite image_2;
    public Sprite image_1;
    public Sprite image_start;

    public GameObject timer;
    public GameObject Players;
    public GameObject BGM;

    private float count;

    private FadeEffect fade;
    [SerializeField]
    Image fadeImg;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        
        fade = gameObject.AddComponent<FadeEffect>();
        fadeImg.color = new Color(fadeImg.color.r,
                                  fadeImg.color.g,
                                  fadeImg.color.b,
                                  1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeImg.color.a > 0f)
            fadeImg.color = new Color(fadeImg.color.r, 
                                      fadeImg.color.g,
                                      fadeImg.color.b,
                                      fade.FadeIN(true));
        else count += Time.deltaTime;

        var img = GetComponent<Image>();
        SE_OneShot SE_Con = BGM.GetComponent<SE_OneShot>();
        SoundCtrl Sound_Con = BGM.GetComponent<SoundCtrl>();

        switch (count)
        {
            case float n when n >= 0.03 && n < 1:
                img.color = new Color(255, 255, 255, 255);
                SE_Con.Count_SE(0);
                break;

            case float n when n >= 1 && n < 2:
                img.sprite = image_2;
                SE_Con.Count_SE(1);
                break;

            case float n when n >= 2 && n < 3:
                img.sprite = image_1;
                SE_Con.Count_SE(2);
                break;

            case float n when n >= 3 && n < 4:
                img.sprite = image_start;
                SE_Con.Count_SE(3);
                break;

            case float n when n > 4:
                timer.gameObject.SetActive(true);
                Players.GetComponent <PlayerMove> ().enabled = true;
                Sound_Con.SetBGM(0);
                Sound_Con.SetActionSE(0);
                this.gameObject.SetActive(false);
                break;
        }
    }

}

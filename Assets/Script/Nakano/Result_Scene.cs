using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result_Scene : MonoBehaviour
{
    [SerializeField]
    string nextScene = "NewScene";

    [SerializeField] Text Move_T;

    private float timer;
    public float maxTime;

    private FadeEffect fade;
    [SerializeField]
    Image fadeImg;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        fade = gameObject.AddComponent<FadeEffect>();
        fadeImg.color = new Color(fadeImg.color.r,
                                    fadeImg.color.g,
                                    fadeImg.color.b,
                                    255);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeImg.color.a > 0f && timer == 0)
            fadeImg.color = new Color(fadeImg.color.r,
                                    fadeImg.color.g,
                                    fadeImg.color.b,
                                    fade.FadeIN(true));
        else if (timer <= maxTime)
        {
            timer += Time.deltaTime;
            Move_T.text = (maxTime - timer).ToString("f0");
        }
        if (timer > maxTime)
        {
            if (fadeImg.color.a < 1f)
                fadeImg.color = new Color(fadeImg.color.r,
                                        fadeImg.color.g,
                                        fadeImg.color.b,
                                        fade.FadeOUT(true));
            else if(fadeImg.color.a >= 1f)
            {
                Invoke("SceneLoader", 0.3f);
            }
        }
    }
    void SceneLoader()
    {
        SceneManager.LoadScene(nextScene);
    }
}

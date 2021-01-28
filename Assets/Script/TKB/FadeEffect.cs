using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    Image fadeImage;

    float fadeINA;
    float fadeOUTA;
    float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fadeINA = 255f;
        fadeOUTA = 0f;
        fadeSpeed = 5.05f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float FadeOUT(bool on)
    {
        if (on && fadeOUTA <= 1f)
        {
            fadeOUTA += 0.001f;
            fadeOUTA *= (fadeSpeed * Time.deltaTime) + 1;
        }
        return fadeOUTA;
    }

    public float FadeIN(bool on)
    {
        if (on && fadeINA >= 0f)
        {
            fadeINA -= 0.001f;
            fadeINA /= (fadeSpeed * Time.deltaTime) + 1;
        }
        return fadeINA;
    }
}

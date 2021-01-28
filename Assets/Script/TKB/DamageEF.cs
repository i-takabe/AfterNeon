using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEF : MonoBehaviour
{
    [SerializeField]
    GameObject prefub;
    [SerializeField]
    Image damageSprite;
    public float blinkingSpeed = 1f; //点滅速度
    float alpha;
    float f;
    Vector4 spriteAlpha;

    // Start is called before the first frame update
    void Start()
    {
        f = 0;
        alpha = 0f;
        spriteAlpha = new Vector4(1, 1, 1, alpha);
        damageSprite.color = spriteAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        if (f < 180)
        {
            f += blinkingSpeed;
            spriteAlpha.w = Mathf.Sin(Mathf.Deg2Rad * f);
            damageSprite.color = spriteAlpha;            
        }
        else
        {
            Destroy(prefub);      
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI_Con : MonoBehaviour
{ 
    Image speedUI_fill;

    [SerializeField]
    float maxSpeed = 283f;
    private float currentSpeed = 0f;
    private Vector3 oldPlayerPos;
    private Vector3 newPlayerPos;
    [SerializeField]
    GameObject player;

    public Color Max_Color;
    public Color Middle_Color;
    public Color Little_Color;

    [SerializeField]
    private float[] Speed_Color;

    // Start is called before the first frame update
    void Start()
    {
        speedUI_fill = GetComponent<Image>();
        newPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPlayerPos = player.transform.position;

        currentSpeed = SpeedGetter();
        SpeedUIcontroller(currentSpeed);

        oldPlayerPos = newPlayerPos;
    }

    float SpeedGetter()
    {
        float speed;
        speed = ((newPlayerPos.z - oldPlayerPos.z) / Time.deltaTime);
        return speed;
    }

    void SpeedUIcontroller(float speed)
    {
        speedUI_fill.fillAmount = speed / maxSpeed;
        if(speedUI_fill.fillAmount >= Speed_Color[0])
        {
            speedUI_fill.color = Max_Color;
        }
        else if(speedUI_fill.fillAmount >= Speed_Color[1] && speedUI_fill.fillAmount < Speed_Color[0])
        {
            speedUI_fill.color = Middle_Color;
        }
        else
        {
            speedUI_fill.color = Little_Color;
        }
    }
}

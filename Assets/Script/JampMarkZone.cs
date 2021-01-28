using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JampMarkZone : MonoBehaviour
{
    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    [SerializeField, Header("最大距離")]
    private float 
        maxZone;
    private float
        speed;
    private Vector3 
        startPos,
        endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        endPos = startPos;
        endPos.z -= maxZone;
    }

    // Update is called once per frame
    void Update()
    {
        speed = paramClass.playerSpeed / paramClass.maxSpeed;
        if (float.IsNaN(speed))
            speed = 0;
        if (speed > 1)
            speed = 1;
        transform.position = Vector3.Lerp(startPos, endPos, speed);
    }
}

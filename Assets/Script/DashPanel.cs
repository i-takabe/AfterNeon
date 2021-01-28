using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    PlayerParamClass
     paramClass = PlayerParamClass.GetInstance();

    [Range(0.0f, 25.0f)]
    public float
        PanelSpeed = 1.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            paramClass.SpeedFluctuation(PanelSpeed);
        }
    }
}

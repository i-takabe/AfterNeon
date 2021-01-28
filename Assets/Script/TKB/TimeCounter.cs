using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    float stageSpendTime;

    // Start is called before the first frame update
    void Start()
    {
        stageSpendTime = 0f;
    }

    public float TimeGeter()
    {
        stageSpendTime += Time.deltaTime;
        return stageSpendTime;
    }
}
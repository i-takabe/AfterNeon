using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    TimeCounter time;
    [SerializeField] Text TimeText;
    public static float stageTime;

    // Start is called before the first frame update
    void Start()
    {       
        time = gameObject.AddComponent<TimeCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        stageTime = time.TimeGeter();
        TimeText.text = stageTime.ToString("f2");
    }
}

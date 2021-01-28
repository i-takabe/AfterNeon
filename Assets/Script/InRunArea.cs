using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRunArea : MonoBehaviour
{
    [SerializeField, Header("エリアの高さ")]
    private float
          areaHight;
    private bool
        rightFootUp,
        oldrightState,
        leftFootUp,
        oldleftState,
        runNow;
    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        AreaPositionSet();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            AreaPositionSet();
        if (oldleftState != leftFootUp || oldrightState != rightFootUp)
        {
            //走る判定
            paramClass.isRun = true;
            Debug.Log("IN");
        }
        oldleftState = leftFootUp;
        oldrightState = rightFootUp;
    }

    private void AreaPositionSet() {
        Transform mytransform = this.transform;
        Vector3 area_move = mytransform.position;
        area_move.y = paramClass.playerPos.y - areaHight;
        mytransform.position = area_move;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FootLeft")
        {
            leftFootUp = false;
        }
        if (other.tag == "FootRight")
        {
            rightFootUp = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FootLeft")
        {
            leftFootUp = true;
        }
        if (other.tag == "FootRight")
        {
            rightFootUp = true;
        }
    }
}

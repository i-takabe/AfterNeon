using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunS : MonoBehaviour
{
    PlayerParamClass
    paramClass = PlayerParamClass.GetInstance();

    [SerializeField, Header("壁走りに最低限必要な速度")]
    private float wallRunSpeed = 10.0f;
    [SerializeField, Header("エンド位置")]
    private Transform endPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && paramClass.playerSpeed >= wallRunSpeed)
        {
            paramClass.isWallRun = true;
            if (endPos != null)
                paramClass.SetWallRun(this.transform.position, endPos.position);
        }
    }
}
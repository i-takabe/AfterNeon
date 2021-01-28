using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    //加速減速
    [SerializeField, Header("加速値")]
    private float
        accele;
    [SerializeField, Header("最高速度値")]
    private float
    fullSpeed;
    [SerializeField, Header("左右加速値")]
    private float
        accele_LR;
    [SerializeField, Header("減速値"), Tooltip("0:通常 1:ジャンプ＆スライディング時 2:壁走り中")]
    private float[]
        decele;

    [SerializeField, Header("ジャンプ力")]
    private float
        jumpForce;

    private sbyte
        setDecele;
    private Rigidbody
        playerRB;
    private CapsuleCollider
        playerCol;
    private Vector3
        pColCenter;
    private float
        pColHeight,
        wallRunTimer;
    private IEnumerator
        regularlyUpdate;       

    private enum MyFunction
    {
        DECELERATE,
        CORRECTION_LR,
        MAXVAL
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCol = GetComponent<CapsuleCollider>();
        pColCenter = playerCol.center;
        pColHeight = playerCol.height;

        SetDecele(0);
        paramClass.maxSpeed = fullSpeed;

        //減速処理のコルーチン呼び出し
        regularlyUpdate = RegularlyUpdate(MyFunction.DECELERATE);
        StartCoroutine(regularlyUpdate);
    }
    private void FixedUpdate()
    {
        ActionPlayerMove();
    }
    void Update()
    {
        if (paramClass.isWallRun)
        {
            WallRunMove();
            SetDecele(2);
            return;
        }
        //加速入力
        if ((paramClass.isRun || Input.GetKeyDown(KeyCode.UpArrow)) && paramClass.isGround)
        { 
            paramClass.SpeedFluctuation(accele);
            paramClass.isRun = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || paramClass.statusLR == PlayerParamClass.LRTrigger.RIGHT)
            paramClass.SpeedFluctuation_LR(accele_LR / 60f);
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || paramClass.statusLR == PlayerParamClass.LRTrigger.LEFT)
            paramClass.SpeedFluctuation_LR(-accele_LR / 60f);
        else
            paramClass.SpeedFluctuation_LR(0);
        if ((Input.GetKeyDown(KeyCode.Space) || paramClass.isJump) && paramClass.isGround)
        {
            paramClass.SpeedFluctuation_Jump(jumpForce);
            SetDecele(1);
            Debug.Log("jump");
        }
        else if(!paramClass.isGround)
            paramClass.SpeedFluctuation_Jump(0);
        if(!paramClass.isJump)
            SetDecele(0);
        if (Input.GetKey(KeyCode.LeftControl) || paramClass.isSliding)
        {
            playerCol.height = pColHeight / 2f;
            playerCol.center = new Vector3(0, pColCenter.y-(pColHeight/2 - playerCol.height/2), 0);
            SetDecele(1);
        }
        else if (playerCol.height != pColHeight)
        {
            playerCol.height = pColHeight;
            playerCol.center = pColCenter;
            SetDecele(0);
        }
    }

    //プレイヤーを動かす
    private void ActionPlayerMove()
    {       
        Vector3 movePos = Vector3.zero;

        //前方移動
        movePos.z = paramClass.playerSpeed;
        Debug.Log(paramClass.playerSpeed);

        //左右移動
        movePos.x = paramClass.playerSpeed_LR;

        //とりあえずジャンプ
        movePos.y = paramClass.playerJumpforce;

        if (!paramClass.isGround)
            movePos.y = playerRB.velocity.y; //空中時にY要素のみ変化なし（自由落下）
        
        playerRB.velocity = movePos;   
    }

    /// <summary>
    /// 定期的に更新する処理用のコルーチン
    /// *2回目以降はUpdateの処理後に実行するので注意*
    /// </summary>
    /// <param name="val">実行する処理</param>
    /// <returns>何秒ごとに実行するか(1.0f = 1秒)</returns>
    IEnumerator RegularlyUpdate(MyFunction val)
    {
        switch (val)
        {
            case MyFunction.DECELERATE:
                for (;;)
                {
                    Decelerate();
                    yield return new WaitForSeconds(.1f);
                }
            case MyFunction.CORRECTION_LR:
                for(;;)
                {
                    Correction_LR();
                    yield return new WaitForSeconds(.1f);
                }
            default:
                break;
        }
    }
    /// <summary>
    /// 選んだ減速値をセットする
    /// 0:通常 1:ジャンプ＆スライディング時 2:壁走り中
    /// </summary>
    /// <param name="num"></param>
    private void SetDecele(sbyte num)
    {
        setDecele = num;
    }
    //減速させる
    private void Decelerate()
    {
        paramClass.SpeedFluctuation(decele[setDecele]);
    }
    private void Correction_LR()
    {

    }
    private void WallRunMove()
    {
        wallRunTimer += (paramClass.playerSpeed / 120.0f) / (paramClass.wallRunEndPos.z - paramClass.wallRunStartPos.z);
        transform.position = Vector3.Lerp(paramClass.wallRunStartPos, paramClass.wallRunEndPos, wallRunTimer);
        if (wallRunTimer >= 1)
        {
            paramClass.isWallRun = false;
            SetDecele(0);
            wallRunTimer = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのパラメーター管理クラス
/// </summary>
public class PlayerParamClass
{
    private static PlayerParamClass playerParamClass;

    /// <summary>
    /// コンストラクタ（初期化）
    /// </summary>
    public PlayerParamClass()
    {
        InitParam();
    }
    public void InitParam()
    {
        playerLife = 3;
        playerSpeed = 0;
        playerSpeed_LR = 0;
        playerJumpforce = 0;
        statusLR = LRTrigger.STAY;
        maxSpeed = 0;
        isWallRun = false;
    }

    /// <summary>
    /// PlayerParamClassをシングルトンパターンでインスタンス化
    /// </summary>
    /// <returns></returns>
    public static PlayerParamClass GetInstance()
    {
        //singleton
        if (playerParamClass == null)
        {
            playerParamClass = new PlayerParamClass();
        }
        return playerParamClass;
    }
    public enum LRTrigger
    {
        LEFT,
        RIGHT,
        STAY
    }
    public LRTrigger
        statusLR;
    public sbyte
        playerLife
    { get; private set; }
    const sbyte MaxLife = 3;
    
    public float
        playerSpeed
    { get; private set; }
    public float
        playerSpeed_LR
    { get; private set; }
    public float
        playerJumpforce
    { get; private set; }
    
    public Vector3
        playerPos
    { get; private set; }
    public Vector3
    wallRunStartPos
    { get; private set; }
    public Vector3
    wallRunEndPos
    { get; private set; }
    
    public bool
        isRun,
        isJump,
        isGround,
        isSliding,
        isWallRun;
    private bool
        rightKneeUpNow, 
        leftKneeUpNow;
    public float
        maxSpeed;

    const float
        KneeSetUp = 76;
    /// <summary>
    /// プレイヤーのライフ変動を格納
    /// </summary>
    /// <param name="val">変動する値</param>
    public void LifeFluctuation(sbyte val)
    {
        playerLife += val;
        if (playerLife > MaxLife)
            playerLife = MaxLife;
        //ライフがなくなったらGameOverへ遷移
        if (playerLife <= 0)
            playerDie();
    }
    private void playerDie()
    {
        Debug.Log("GameOver");
    }

    /// <summary>
    /// プレイヤーの速度変動を格納
    /// </summary>
    /// <param name="val">変動する値</param>
    public void SpeedFluctuation(float val)
    {
            playerSpeed += val;
        if (playerSpeed > maxSpeed)
            playerSpeed = maxSpeed;
        if (playerSpeed < 0)
            playerSpeed = 0;
    }

    public void SetPos(Vector3 pos)
    {
        playerPos = pos;
    }
    public void SpeedFluctuation_LR(float val)
    {
        playerSpeed_LR += val;
        if (val == 0)
            playerSpeed_LR = 0;
    }

    public void SpeedFluctuation_Jump(float val)
    {
        playerJumpforce = val;
    }

    private bool triggerRL;//false:L true:R
    /// <summary>
    /// 走っているか決める
    /// </summary>
    /// <param name="val">角度</param>
    public void RunAngleJudge(float val)
    {
        if (triggerRL)
        {
            if (val > KneeSetUp + 1 && !rightKneeUpNow)
            {
                rightKneeUpNow = true;
                isRun = true;
            }
            if (val <= KneeSetUp && rightKneeUpNow)
                rightKneeUpNow = false;
        }
        else
        {
            if (val > KneeSetUp + 1 && !leftKneeUpNow)
            {
                leftKneeUpNow = true;
                isRun = true;
            }
            if (val <= KneeSetUp && leftKneeUpNow)
                leftKneeUpNow = false;

        }
        triggerRL = !triggerRL;
    }

    public void SetWallRun(Vector3 startPos, Vector3 endPos)
    {
        wallRunStartPos = startPos;
        wallRunEndPos = endPos;
    }
}
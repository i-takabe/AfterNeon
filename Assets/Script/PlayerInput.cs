using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightBuzz.Kinect4Azure;

public class PlayerInput : MonoBehaviour
{
    PlayerParamClass
    paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    private Transform
        bodyT;
    private Dictionary<JointType, JointType>
        parentJointMap = new Dictionary<JointType, JointType>() {
        { JointType.Pelvis, JointType.Head },
        { JointType.SpineNaval, JointType.Pelvis },
        { JointType.SpineChest, JointType.SpineNaval },
        { JointType.Neck, JointType.Head },
        { JointType.ClavicleLeft, JointType.SpineChest },
        { JointType.ShoulderLeft, JointType.ClavicleLeft },
        { JointType.ElbowLeft, JointType.ShoulderLeft },
        { JointType.WristLeft, JointType.ElbowLeft },
        { JointType.HandLeft, JointType.WristLeft },
        { JointType.HandtipLeft, JointType.HandLeft },
        { JointType.ThumbLeft, JointType.Head },
        { JointType.ClavicleRight, JointType.ClavicleRight },
        { JointType.ShoulderRight, JointType.ClavicleRight },
        { JointType.ElbowRight, JointType.ShoulderRight },
        { JointType.WristRight, JointType.ElbowRight },
        { JointType.HandRight, JointType.WristRight },
        { JointType.HandtipRight, JointType.HandRight },
        { JointType.ThumbRight, JointType.Head },
        { JointType.HipLeft, JointType.Pelvis },
        { JointType.KneeLeft, JointType.HipLeft },
        { JointType.AnkleLeft, JointType.KneeLeft },
        { JointType.FootLeft, JointType.AnkleLeft },
        { JointType.HipRight, JointType.Pelvis },
        { JointType.KneeRight, JointType.HipRight },
        { JointType.AnkleRight, JointType.KneeRight },
        { JointType.FootRight, JointType.AnkleRight },
        { JointType.Head, JointType.Head },
        { JointType.Nose, JointType.Head },
        { JointType.EyeLeft, JointType.Head },
        { JointType.EarLeft, JointType.Head },
        { JointType.EyeRight, JointType.Head },
        { JointType.EarRight, JointType.Head },
    };

    private KinectSensor sensor;
    private Vector3[] footsSetVal;
    private bool setUpFoot;
    private Vector3 neckJudgeVal;
    [SerializeField, Header("ジャンプ基準の高さ"), Tooltip("首の位置がどれだけ上昇したらジャンプするかの値")]
    private float jumpHight = 0.3f;
    [SerializeField, Header("スライディング基準の高さ"), Tooltip("首の位置がどれだけ下降したらスライディングするかの値")]
    private float slidingHight = 0.3f;
    [SerializeField, Header("横移動の許容値")]
    private float moveTolerance = 0.3f;
    private Vector3 oldPos = Vector3.zero;

    private void Awake()
    {
        UnityEnvironment.Init();
    }
    private void Start()
    {
        sensor = KinectSensor.GetDefault();
        sensor?.Open();

    }

    private void FixedUpdate()
    {
        if (sensor == null || !sensor.IsOpen) return;

        Frame frame = sensor.Update();

        if (frame != null)
        {
            if (frame.BodyFrameSource != null)
            {
                UpdateAvatars(frame.BodyFrameSource.Bodies);
                if (footsSetVal != null && !setUpFoot)
                {
                    setUpFoot = true;
                    Debug.Log(setUpFoot);
                }
            }
        }
    }

    private void OnDestroy()
    {
        sensor?.Close();
    }

    /// <summary>
    /// Updates terrain position and rotation.
    /// Updates all joints positions and bones.
    /// </summary>
    /// <param name="bodies"></param>
    /// <param name="floor"></param>
    private void UpdateAvatars(List<Body> bodies)
    {
        if (bodies == null || bodies.Count == 0) return;

        // Show joints
        Body body = bodies.Closest();

        for (int i = 0; i <= (int)JointType.EyeRight; i++)
        {
            //関節の入力
            Vector3 jointPos = body.Joints[(JointType)i].Position;
            jointPos *= -1f;
            Quaternion jointRotation = body.Joints[(JointType)i].Orientation;

            //関節の出力
            //へその位置くらいでポジションを取る
            if ((JointType)i == JointType.SpineNaval)
                paramClass.SetPos(jointPos);
            if((JointType)i == JointType.Neck)
                NeckJudge(jointPos);
            //膝と腰の角度で判定
            //if ((JointType)i == JointType.HipRight || (JointType)i == JointType.HipLeft)
            //    KneeAngle( jointPos, body.Joints[(JointType)i+1].Position);
            
            //高さの判定
            //初期セットアップ
            if (Input.GetKeyDown(KeyCode.R) || footsSetVal == null)
            {
                footsSetVal = new Vector3[3];
                footsSetVal[0] = body.Joints[JointType.FootLeft].Position;
                footsSetVal[1] = body.Joints[JointType.FootRight].Position;
                footsSetVal[2] = (footsSetVal[0] + footsSetVal[1]) / 2f;
                neckJudgeVal = body.Joints[JointType.Neck].Position * -1f;
            }
            //if (((JointType)i == JointType.FootLeft || (JointType)i == JointType.FootRight) && setUpFoot)
            //    FootJudge(jointPos);
            Transform jointT = bodyT.GetChild(i);
            jointT.localPosition = jointPos;
            jointT.localRotation = jointRotation; 
            Transform jointBoneT = jointT.GetChild(0);
            if (parentJointMap[(JointType)i] != JointType.Head)
            {
                // set bone
                Vector3 jointParentPos = body.Joints[parentJointMap[(JointType)i]].Position;
                jointParentPos *= -1f;
                Vector3 boneDirection = jointPos - jointParentPos;
                Vector3 boneDirectionLocalSpace = Quaternion.Inverse(jointT.rotation) * Vector3.Normalize(boneDirection);

                jointBoneT.position = jointPos - 0.5f * boneDirection + bodyT.parent.position; ;
                jointBoneT.localRotation = Quaternion.FromToRotation(Vector3.up, boneDirectionLocalSpace);
                jointBoneT.localScale = new Vector3(1f, 20f * 0.5f * boneDirection.magnitude, 1f);
            }
            else
            {
                jointBoneT.gameObject.SetActive(false);
            }
        }
        InputLR();
    }
    private void NeckJudge(Vector3 val)
    {
        Debug.Log(neckJudgeVal.y);
        if (!paramClass.isJump && val.y > neckJudgeVal.y + jumpHight)
            paramClass.isJump = true;

        if (!paramClass.isSliding && val.y < neckJudgeVal.y - slidingHight)
            paramClass.isSliding = true;
            //Debug.Log("slidingNoW!!");
        else if (val.y > neckJudgeVal.y - slidingHight)
            paramClass.isSliding = false;
    }
    private void InputLR()
    {
        Vector3 pos = oldPos - paramClass.playerPos;
        if (pos.x < -moveTolerance)
            paramClass.statusLR = PlayerParamClass.LRTrigger.RIGHT;      //LEFTとRIGHT入れ替えました(11/27)
        else if(pos.x > moveTolerance)
            paramClass.statusLR = PlayerParamClass.LRTrigger.LEFT;      //LEFTとRIGHT入れ替えました(11/27)
        else
            paramClass.statusLR = PlayerParamClass.LRTrigger.STAY;

        oldPos = paramClass.playerPos;
    }
    private void KneeAngle(Vector3 hipPos, Vector3 kneePos)
    {
        float angleY, angleZ;
        angleY = kneePos.y - hipPos.y;
        angleZ = kneePos.z - hipPos.z;

        float rad = Mathf.Atan2(angleZ, angleY);
        float deg = rad * Mathf.Rad2Deg;

        if (deg < 0)
            deg += 360;
        //if (Input.GetMouseButton(1))
        //    Debug.Log(deg);
        paramClass.RunAngleJudge(deg);
    }
    private bool triggerRL;//false:L true:R
    private bool
    rightFootUpNow, leftFootUpNow, oldR, oldL;
    private void FootJudge(Vector3 val)
    {
        Debug.Log(footsSetVal[0].y + "" + val.y);
        if (val.y > footsSetVal[0].y + 0.05f)
        {
            if (triggerRL)
            {
                rightFootUpNow = true;
                Debug.Log("Rup");
            }
            else
            {
                leftFootUpNow = true;
            }
        }
        else
        {
            if (triggerRL)
                rightFootUpNow = false;
            else
                leftFootUpNow = false;
        }
        if ((rightFootUpNow && !leftFootUpNow) || (!rightFootUpNow && leftFootUpNow))
        {
            paramClass.isRun = true;
        }
    }
}
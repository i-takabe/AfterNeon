using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSystem : MonoBehaviour
{

    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    [SerializeField]
    string nextScene = "NewScene";

    public GameObject ClearLogo;
    public GameObject Players;
    public GameObject Time;
    public GameObject BGM;

    private void OnTriggerEnter(Collider other)
    {
        SE_OneShot SE_Con = BGM.GetComponent<SE_OneShot>();
        SoundCtrl Sound_Con = BGM.GetComponent<SoundCtrl>();

        if (other.CompareTag("Player"))
        {
            Sound_Con.SetBGM(1);
            Sound_Con.SetActionSE(1);
            SE_Con.Goal();

            //クリアしたよ表示
            ClearLogo.gameObject.SetActive(true);

            //しばらくしたらリザルトへ
            Invoke("ChangeScrene", 6f);

            //今回のタイムをセーブしとく
            PlayerPrefs.SetFloat("NewScore", StateUI.stageTime);
            PlayerPrefs.Save();

            Players.GetComponent<PlayerMove>().enabled = false;
            Time.gameObject.SetActive(false);
        }
    }

    void ChangeScrene()
    {
        paramClass.InitParam();
        SceneManager.LoadScene(nextScene);
    
    }

}

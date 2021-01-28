using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text YouScoreText;

    [SerializeField, Header("表示用テキスト")]
    Text[] Rankingtext = new Text[5];

    [SerializeField] Text Resetcall;

    [SerializeField]
    GameObject[] Ranking_Bar = new GameObject[5];

    private float[] RankScore = new float[5];

    private bool Change;

    public GameObject New;

    // Start is called before the first frame update
    void Start()
    {
        Change = true;
        float YourScore = PlayerPrefs.GetFloat("NewScore");
        YouScoreText.text = YourScore.ToString("f2");

        for (int i = 0; i < 5; i++)
        {
            RankScore[i] = PlayerPrefs.GetFloat("Score_" + i, 0);
        }
        
        for(int i = 0; i < 5; i++)
        {
            if (Change && (YourScore <= RankScore[i] || RankScore[i] == 0))
            {
                for (int j = 4; j > i; j--)
                {
                    RankScore[j] = RankScore[j - 1];
                }

                RankScore[i] = YourScore;
                Ranking_Bar[i].gameObject.SetActive(true);
                New.gameObject.SetActive(true);
                Change = false;
            }
            Rankingtext[i].text = RankScore[i].ToString("f2");
        }

        RankSave();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Resetcall.text = "Ranking Reset";
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetFloat("Score_" + i, 0);
            }
            PlayerPrefs.Save();
        }
    }

    //ランキングセーブ
    void RankSave()
    {
        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("Score_" + i, RankScore[i]);
        }
        PlayerPrefs.Save();
    }

}

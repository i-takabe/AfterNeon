using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    Image Map_fill;

    [SerializeField]
    float goalPos = 1540;
    private Vector3 PlayerPos;
    private Vector3 MapPos;
    [SerializeField]
    GameObject MapHuman;
    [SerializeField]
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        Map_fill = GetComponent<Image>();
        PlayerPos = player.transform.position;
        MapPos = MapHuman.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerPos = player.transform.position;
        MapUICon(PlayerPos.z);

    }

    void MapUICon(float pos)
    {
        Map_fill.fillAmount = pos / goalPos;
        MapPos.x = (pos / goalPos) * 925 + 80;

        MapHuman.transform.position = MapPos;
    }
}

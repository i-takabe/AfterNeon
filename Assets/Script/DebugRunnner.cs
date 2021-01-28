using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugRunnner : MonoBehaviour
{

    PlayerParamClass
    paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    Vector3 ContinuePos;
    public bool AutoRun;

    [Range(0.0f, 25.0f)]
    public float
        AutoRunSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AutoRun && paramClass.playerSpeed < AutoRunSpeed)
            paramClass.isRun = true;
        if (Input.GetKey(KeyCode.C))
            Continue();
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
            LoadReset();
    }

    private void Continue()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        pos.x = ContinuePos.x;
        pos.y = ContinuePos.y;
        pos.z = ContinuePos.z;
        myTransform.position = pos;
    }

    private void LoadReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContinuePoint"))
            Continue();
    }
}

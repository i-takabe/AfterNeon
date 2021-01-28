using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_particle : MonoBehaviour
{

    public ParticleSystem Dmg_EF;
    public ParticleSystem Dmg_EF2;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Damage!");
            Dmg_EF.Play();
            Dmg_EF2.Play();
        }
    }
}

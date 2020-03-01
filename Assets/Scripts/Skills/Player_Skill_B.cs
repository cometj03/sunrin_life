using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_B : MonoBehaviour
{
    GameObject Player;
    public ParticleSystem ps;
    private float speed;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        ps = GetComponentInChildren<ParticleSystem>();

        speed = 7f;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        if (ps != null)
        {
            ParticleSystem.MainModule main = ps.main;
            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                main.startRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

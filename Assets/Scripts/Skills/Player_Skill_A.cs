using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_A : MonoBehaviour
{
    public GameObject Particle_A;
    GameObject Player;
    private float speed, angle, difficulty;

    HPManager hpManager;

    void Start()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        if (hpManager == null)
            hpManager = GameObject.Find("HP Gauge").GetComponent<HPManager>();
        speed = 12f;
        angle = 0f;
        difficulty = (int)DataManager.instance.currentStage;
    }

    void Update()
    {
        // Arrow_angle 값 받아오기
        angle = Player.GetComponent<ArrowRotation>().Arrow_angle;

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            hpManager.PlusHP(14f - difficulty * 2);
            Vector2 pos = collision.transform.position;
            pos.y = transform.position.y + 0.5f;
            var clone = Instantiate(Particle_A, pos, Quaternion.Euler(0, 0, angle));
            Destroy(clone, 1.0f);
            Destroy(gameObject);
        }
        if (collision.tag == "Monster")
        {
            hpManager.PlusHP(3f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_A : MonoBehaviour
{
    public GameObject Particle_A;
    GameObject Player;
    private float speed, angle;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        speed = 12f;
        angle = 0f;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Arrow_angle 값 받아오기
        angle = Player.GetComponent<ArrowRotation>().Arrow_angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(-3f);
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(16f);

            var clone = Instantiate(Particle_A, transform.position, Quaternion.Euler(0, 0, angle + 75f));
            Destroy(clone, 1.0f);
            Destroy(gameObject);
        }
    }
}

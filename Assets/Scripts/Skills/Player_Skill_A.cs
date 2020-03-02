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
        // Arrow_angle 값 받아오기
        angle = Player.GetComponent<ArrowRotation>().Arrow_angle;

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            //GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(-3f);
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(8f);
            Vector2 pos = collision.transform.position;
            pos.y = transform.position.y + 0.5f;
            var clone = Instantiate(Particle_A, pos, Quaternion.Euler(0, 0, angle));
            Destroy(clone, 1.0f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_A : MonoBehaviour
{
    private float speed;

    void Awake()
    {
        speed = 12f;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
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
            Destroy(gameObject);
        }
    }
}

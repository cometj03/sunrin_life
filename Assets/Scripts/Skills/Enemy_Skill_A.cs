using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skill_A : MonoBehaviour
{
    private float speed;
    void Awake()
    {
        speed = 12f;
    }

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(-10f);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Shield")
        {
            GameObject.Find("HP Gauge").GetComponent<HPManager>().PlusHP(20f);
            Destroy(gameObject);
        }
    }
}

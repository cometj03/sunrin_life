using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skill_A : MonoBehaviour
{
    private float speed, damage;
    int difficulty;
    HPManager hpManager;

    void Start()
    {
        if (hpManager == null)
            hpManager = GameObject.Find("HP Gauge").GetComponent<HPManager>();
        speed = 12f;
        difficulty = (int)DataManager.instance.currentStage;
        damage = (difficulty + 2) * 4;
    }

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hpManager.PlusHP(-damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Shield")
        {
            hpManager.PlusHP(20f - difficulty * 2);
            Destroy(gameObject);
        }
    }
}

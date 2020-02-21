using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_A : MonoBehaviour
{
    private float speed;

    void Awake()
    {
        speed = 10f;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {

        }
    }
}

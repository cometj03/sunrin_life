using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpMove : MonoBehaviour
{
    Animator anim;
    GameObject Player;

    HPManager hpManager;

    bool Rainbow;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        if (hpManager == null)
            hpManager = GameObject.Find("HP Gauge").GetComponent<HPManager>();

        anim = transform.GetComponent<Animator>();
        Rainbow = true;
    }

    void Update()
    {
        Vector3 target = Player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
        if (transform.position.y <= target.y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Skill_A")
        {
            anim.SetBool("ColorChange", true);
            Rainbow = false;
        }
        if (collision.tag == "Player")
        {
            if (Rainbow)
                hpManager.PlusHP(-15f);
            else
                hpManager.PlusHP(-5f);
            Destroy(gameObject);
        }
        if (collision.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
}

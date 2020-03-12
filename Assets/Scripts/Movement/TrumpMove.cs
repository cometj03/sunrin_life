using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpMove : MonoBehaviour
{
    Animator anim;
    GameObject Player;

    HPManager hpManager;
    PlayerMove playerMove;

    bool Rainbow;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        if (hpManager == null)
            hpManager = GameObject.Find("HP Gauge").GetComponent<HPManager>();
        if (playerMove == null)
            playerMove = Player.GetComponent<PlayerMove>();

        anim = transform.GetComponent<Animator>();
        Rainbow = true;
    }

    void Update()
    {
        Vector3 target = Player.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
        Vector3 velo = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.15f);
        if (transform.position.y <= target.y + 0.8f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Skill_A")
        {
            if (Rainbow)
            {
                anim.SetBool("ColorChange", true);
                Rainbow = false;
            }
            else
                Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            if (Rainbow)
                hpManager.PlusHP(-15f);

            // 공포
            playerMove.flee = -1;
            Destroy(gameObject);
        }
        if (collision.tag == "Shield")
        {
            if (Rainbow)
            {
                anim.SetBool("ColorChange", true);
                Rainbow = false;
            }
            else
                Destroy(gameObject);

            Destroy(collision.gameObject);
        }
    }
}

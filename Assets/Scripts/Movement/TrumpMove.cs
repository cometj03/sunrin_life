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
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.12f);
        if (transform.position.y <= target.y + 0.7f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shield")
        {
            anim.SetBool("ColorChange", true);
            Rainbow = false;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            if (Rainbow)
                hpManager.PlusHP(-15f);
            else
                hpManager.PlusHP(-5f);

            // 공포
            playerMove.flee = -1;
            Destroy(gameObject);
        }
    }
}

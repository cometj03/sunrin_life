using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Skill_A;
    
    private bool canShoot_a;
    public bool playerMoveable, arrowMoveable;
    private float angle;

    Quaternion rotation;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        canShoot_a = true;
        playerMoveable = true;
        arrowMoveable = true;
        angle = 0;
    }

    void Update()
    {
        angle = Player.gameObject.GetComponent<ArrowRotation>().Arrow_angle;
        if (Input.GetKeyDown(KeyCode.Q) && canShoot_a)
        {
            StartCoroutine("SpawnA");
            StartCoroutine("Pause");
        }
    }

    IEnumerator SpawnA()
    {
        Vector3 pos = Player.transform.position;
        pos.y += 1.5f;
        Instantiate(Skill_A, pos, Quaternion.Euler(0, 0, angle));
        canShoot_a = false;
        yield return new WaitForSeconds(0.7f);
        canShoot_a = true;
    }

    // 0.1s pause moving
    IEnumerator Pause()
    {
        playerMoveable = false;
        arrowMoveable = false;
        yield return new WaitForSeconds(0.2f);
        playerMoveable = true;
        arrowMoveable = true;
    }
}

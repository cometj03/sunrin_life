using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Player_Skill_A;
    
    private float cool_a, cool_b;
    public bool playerMoveable, arrowMoveable;
    private float angle;

    Quaternion rotation;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        cool_a = 0;
        cool_b = 0;
        playerMoveable = true;
        arrowMoveable = true;
        angle = 0;
    }

    void Update()
    {
        // 스킬 A 쿨타임
        if (cool_a <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Player_Spawn_A();
                StartCoroutine(Pause(0.2f));
                cool_a = 0.7f;
            }
        }else
        {
            cool_a -= Time.deltaTime;
        }

        // 스킬 B 쿨타임
        if (cool_b <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.GetComponent<PlayerMove>().Flash();
                cool_b = 5.0f;
            }
        }else
        {
            cool_b -= Time.deltaTime;
        }
        
        // Arrow_angle 값 받아오기
        angle = Player.GetComponent<ArrowRotation>().Arrow_angle;
    }

    private void Player_Spawn_A()
    {
        Vector2 pos = Player.transform.position;
        pos.y += 1.5f;
        Instantiate(Player_Skill_A, pos, Quaternion.Euler(0, 0, angle));
    }

    // pause moving
    IEnumerator Pause(float time)
    {
        playerMoveable = false;
        arrowMoveable = false;
        yield return new WaitForSeconds(time);
        playerMoveable = true;
        arrowMoveable = true;
    }
}

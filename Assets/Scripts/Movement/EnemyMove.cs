using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject Enemy_Skill_A;
    GameObject Player;
    enum State { left, stop, right };
    State state = State.stop;

    float howLong, point;
    Vector2 moveVector;
    float speed;

    Animator anim;
    float cool_a, coolTimer_a;

    // EngTEnemy.cs
    protected float cool_Trump, coolTimer_Trump;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        howLong = 0f;
        point = Random.Range(0.0f, 4.0f);
        moveVector = Vector2.zero;
        speed = 4f;

        anim = transform.GetComponent<Animator>();

        cool_a = Random.Range(1.0f, 6.0f);
        coolTimer_a = 0f;

        // EngTEnemy.cs
        cool_Trump = Random.Range(5.5f, 9.5f);
        coolTimer_Trump = 0f;
    }

    void Update()
    {
        // 이동 코드
        if (point > howLong)
        {
            // 밖으로 나가지 못하게
            if (state == State.left && transform.position.x < -8)
                state = State.right;
            else if (state == State.right && transform.position.x > 8)
                state = State.left;
            howLong += Time.deltaTime;
        }
        else
        {
            howLong = 0;
            point = Random.Range(0.0f, 4.0f);
            switch (Random.Range(1, 4))
            {
                case 1: state = State.stop; break;
                case 2: state = State.left; break;
                case 3: state = State.right; break;
            }
            anim.SetBool("isWalk", !(state == State.stop));
        }

        state = GameManager.instance.gameState == GameState.Progressing ? state : State.stop;

        if (state == State.left)
        {
            moveVector.x = -1;
            transform.localScale = new Vector3(0.22f, 0.22f, 0);
        }
        else if (state == State.right)
        {
            moveVector.x = 1;
            transform.localScale = new Vector3(-0.22f, 0.22f, 0);
        }
        else if (state == State.stop)
        {
            moveVector.x = 0;
        }

        transform.Translate(moveVector * speed * Time.deltaTime);

        // 스킬 발사 코드
        if (coolTimer_a < cool_a)
        {
            coolTimer_a += Time.deltaTime;
        }
        else if (GameManager.instance.gameState == GameState.Progressing)
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = Player.transform.position;
            pos.y -= 1.5f;
            float angle = -1 * getAngle(pos.x, pos.y, playerPos.x, playerPos.y);

            float dir = Player.GetComponent<PlayerMove>().dir;  // 플레이어가 바라보고 있는 방향
            Enemy_Spawn_A(pos, angle + Random.Range(-10.0f, 25.0f) * dir);

            cool_a = Random.Range(2.5f, 7.0f);
            coolTimer_a = 0f;
        }
    }

    private void Enemy_Spawn_A(Vector2 pos, float angle)
    {
        Instantiate(Enemy_Skill_A, pos, Quaternion.Euler(0, 0, angle));
    }

    private float getAngle(float x1, float y1, float x2, float y2)
    {
        float dx = x1 - x2, dy = y1 - y2;
        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}

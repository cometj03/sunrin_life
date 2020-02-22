using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    enum State { left, stop, right };
    State state = State.stop;

    float howLong, point;
    Vector2 moveVector;
    float speed;
    
    Animator anim;
    
    void Awake()
    {
        howLong = 0f;
        point = Random.Range(0.0f, 4.0f);
        moveVector = Vector2.zero;
        speed = 4f;
        anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (point > howLong)
        {
            // 밖으로 나가지 못하게
            if (Mathf.Abs(transform.position.x) > 8)
            {
                if (state == State.left)
                    state = State.right;
                else if (state == State.right)
                    state = State.left;
            }
            howLong += Time.deltaTime;
        }
        else
        {
            howLong = 0;
            point = Random.Range(0.0f, 4.0f);
            switch (Random.Range(0, 4) % 3)
            {
                case 0: state = State.stop; break;
                case 1: state = State.left; break;
                case 2: state = State.right; break;
            }
            anim.SetBool("isWalk", !(state == State.stop));
        }

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
            moveVector.x = 0;

        transform.Translate(moveVector * speed * Time.deltaTime);        
    }
}

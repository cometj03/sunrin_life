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
    
    void Start()
    {
        howLong = 0f;
        point = Random.Range(0.0f, 4.0f);
        moveVector = Vector2.zero;
        speed = 4f;
    }

    void Update()
    {
        if (point > howLong)
        {
            // TODO: 밖으로 나가지 못하게 코드 짜기
            transform.Translate(moveVector * speed * Time.deltaTime);
            howLong += Time.deltaTime;
        }
        else
        {
            howLong = 0;
            point = Random.Range(0.0f, 4.0f);
            /*switch (Random.Range(0, 3))
            {
                case 0: state = State.left; break;
                case 1: state = State.stop; break;
                case 2: state = State.right; break;
            }*/
            moveVector.x = Random.Range(-1, 2);
        }
    }
}

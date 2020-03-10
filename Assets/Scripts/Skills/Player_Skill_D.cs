using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_D : MonoBehaviour
{
    GameObject Player;
    public GameObject A_Clone;
    private float speed, angle;

    private float time, _time, _size, _upSizeTime;
    private bool canShoot;
    Vector3 originSize;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        speed = 10f;
        angle = 0;
        time = -2;
        _time = -2;
        _size = 2;
        _upSizeTime = 0.2f;
        canShoot = false;

        originSize = transform.localScale;
    }

    void Update()
    {
        if (transform.position.y < 0.1f)
            transform.position += transform.up * speed * Time.deltaTime;
        else
        {
            transform.rotation = Quaternion.identity;   // 각도 초기화

            // 3초 후 바운스
            if (time <= 0)
                transform.localScale = originSize * (1f + time / 4);
            else
            {
                canShoot = true;
                if (time <= _upSizeTime)
                    transform.localScale = originSize * (1 + _size * time);
                else if (time <= _upSizeTime * 2)
                    transform.localScale = originSize * (2 * _size * _upSizeTime + 1 - time * _size);
                else
                {
                    transform.localScale = originSize;
                    time = 0;
                }
            }

            if (_time >= 5)
                Destroy(gameObject);

            time += Time.deltaTime;
            _time += Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canShoot && collision.tag == "Player_Skill_A")
        {
            Destroy(collision.gameObject);
            // Arrow_angle 값 받아오기
            angle = Player.GetComponent<ArrowRotation>().Arrow_angle;
            for (float i = -15; i <= 15; i += 10)
                Instantiate(A_Clone, transform.position, Quaternion.Euler(0, 0, angle + i));
            Destroy(gameObject);
            
        }
    }
}

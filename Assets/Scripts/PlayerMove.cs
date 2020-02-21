using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject SkillManager;
    public float speed;
    public bool canMove;
    private Vector2 moveVector;

    Animator anim;

    void Awake()
    {
        if (SkillManager == null)
            SkillManager = GameObject.Find("SkillManager");
        speed = 4f;
        moveVector = Vector2.zero;
        anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float PlayerX = transform.position.x;
        canMove = true;
        moveVector.x = h;
        
        if (h > 0)  // 오른쪽
        {
            transform.localScale = new Vector3(-0.22f, 0.22f, 1);
            canMove = PlayerX >= 8 ? false : true;
        }
        else if (h < 0) // 왼쪽
        {
            transform.localScale = new Vector3(0.22f, 0.22f, 1);
            canMove = PlayerX <= -8 ? false : true;
        }
        // 애니메이션 제어
        if (h == 0) anim.SetBool("isWalk", false);
        else
        {
            anim.SetBool("isWalk", true);
        }
        // 움직일 수 있는지
        if (canMove && SkillManager.gameObject.GetComponent<SkillManager>().playerMoveable)
            transform.Translate(moveVector * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject SkillManager;
    public float speed;
    public bool canMove;
    private Vector2 moveVector;
    float dir;

    Animator anim;

    void Awake()
    {
        if (SkillManager == null)
            SkillManager = GameObject.Find("SkillManager");
        speed = 4f;
        moveVector = Vector2.zero;
        dir = 0;

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
            canMove = PlayerX > 8 ? false : true;
            dir = 1;
        }else if (h < 0) // 왼쪽
        {
            transform.localScale = new Vector3(0.22f, 0.22f, 1);
            canMove = PlayerX < -8 ? false : true;
            dir = -1;
        }
        // 애니메이션 제어
        anim.SetBool("isWalk", !(h == 0));
        
        // 움직일 수 있는지
        if (canMove && SkillManager.gameObject.GetComponent<SkillManager>().playerMoveable)
            transform.Translate(moveVector * speed * Time.deltaTime);
    }

    Vector2 targetVector;
    public void Flash()
    {
        // dir == 1 : 오른쪽, dir == -1 : 왼쪽
        targetVector = new Vector2(transform.position.x + 4 * dir, transform.position.y);
        if (Mathf.Abs(targetVector.x) > 8)
            targetVector.x = 8 * dir;
        StartCoroutine("Teleport");
    }
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = targetVector;
    }
}

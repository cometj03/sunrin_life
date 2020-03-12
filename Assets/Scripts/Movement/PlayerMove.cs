using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject SkillManager;
    GameObject JoyStick;
    public float speed;
    public bool canMove, isJoyStick;
    private Vector2 moveVector;

    // 바라보고 있는 방향 (1 : right, -1 : left)
    public float dir;

    // 공포 (-1 : true, 1 : false)
    public int flee;
    float fleeTime;

    Animator anim;

    SkillManager skillMgr;

    void Awake()
    {
        if (SkillManager == null)
            SkillManager = GameObject.Find("SkillManager");
        if (JoyStick == null)
            JoyStick = GameObject.Find("JoyStick_BackGround");
        if (skillMgr == null)
            skillMgr = SkillManager.GetComponent<SkillManager>();

        speed = 4f;
        moveVector = Vector2.zero;
        dir = 0;

        anim = transform.GetComponent<Animator>();
        dir = -1;
        flee = 1;
        fleeTime = 0;

#if     UNITY_EDITOR
        isJoyStick = false;
#elif   UNITY_ANDROID
        isJoyStick = true;
#endif
    }

    void Update()
    {
        float h;
        if (isJoyStick)
            h = JoyStick.GetComponent<JoyStick>().JoyVec.x;
        else
            h = Input.GetAxisRaw("Horizontal");
        float PlayerX = transform.position.x;

        // 공포
        if (flee < 0)
        {
            if (fleeTime <= 1.7f)
                fleeTime += Time.deltaTime;
            else
            {
                flee = 1;
                fleeTime = 0;
            }
        }
        h *= flee;

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

        // 게임 진행중 여부
        canMove = GameManager.instance.gameState == GameState.Progressing ? canMove : false;

        // 움직일 수 있는지
        if (canMove && skillMgr.playerMoveable)
            transform.Translate(moveVector * speed * Time.deltaTime);
    }

    Vector2 targetVector;
    public void Flash()
    {
        // dir == 1 : 오른쪽, dir == -1 : 왼쪽
        targetVector = new Vector2(transform.position.x + 4 * dir, transform.position.y);
        if (Mathf.Abs(targetVector.x) > 8)
            targetVector.x = 8 * dir;
        transform.position = targetVector;
    }
}

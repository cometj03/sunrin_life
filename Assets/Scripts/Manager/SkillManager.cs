using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject Player, PlayerAfterImg;
    public GameObject Player_Skill_A;
    public GameObject Player_Skill_B;
    public GameObject Player_Skill_D;

    public GameObject[] BackGrounds;

    public Button buttonA, buttonB, buttonC, buttonD;
    
    public bool playerMoveable, arrowMoveable;

    private float time_a, time_b, time_c, time_d;   // 현재 쿨타임
    private float cool_a, cool_b, cool_c, cool_d;   // 스킬 쿨타임
    private bool shot_a, shot_b, shot_c, shot_d;    // 클릭 플래그
    private float angle;

    Quaternion rotation;

    PlayerMove playerMove;

    void Awake()
    {
        if (Player == null)
            Player = GameObject.Find("Player");
        if (playerMove == null)
            playerMove = Player.GetComponent<PlayerMove>();
        playerMoveable = true;
        arrowMoveable = true;

        time_a = time_b = time_c = time_d = 0;
        shot_a = shot_b = shot_c = shot_d = false;
        cool_a = 1.3f;
        cool_b = 10f;
        cool_c = 5.7f;
        cool_d = 8f;
        angle = 0;
    }

    void Update()
    {
        // Arrow_angle 값 받아오기
        angle = Player.GetComponent<ArrowRotation>().Arrow_angle;

        // 스킬 A 관리
        if (time_a <= 0)
        {
            if (shot_a || Input.GetKeyDown(KeyCode.Q))
            {
                Player_Spawn_A();
                StartCoroutine(Pause(0.1f));
                time_a = cool_a;
            }
            buttonA.image.fillAmount = 1;
            BackGrounds[0].SetActive(true);
        } else
        {
            time_a -= Time.deltaTime;
            buttonA.image.fillAmount = 1 - time_a / cool_a;
            shot_a = false;
            BackGrounds[0].SetActive(false);
        }

        // 스킬 B 관리
        if (time_b <= 0)
        {
            if (shot_b || Input.GetKeyDown(KeyCode.W))
            {
                Player_Spawn_B();
                StartCoroutine(Pause(0.1f));
                time_b = cool_b;
            }
            buttonB.image.fillAmount = 1;
            BackGrounds[1].SetActive(true);
        } else
        {
            time_b -= Time.deltaTime;
            buttonB.image.fillAmount = 1 - time_b / cool_b;
            shot_b = false;
            BackGrounds[1].SetActive(false);
        }

        // 스킬 C 관리
        if (time_c <= 0)
        {
            if (shot_c || Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Player_Flash());
                
                time_c = cool_c;
            }
            buttonC.image.fillAmount = 1;
            BackGrounds[2].SetActive(true);
        } else
        {
            time_c -= Time.deltaTime;
            buttonC.image.fillAmount = 1 - time_c / cool_c;
            shot_c = false;
            BackGrounds[2].SetActive(false);
        }

        // 스킬 D 관리
        if (time_d <= 0)
        {
            if (shot_d || Input.GetKeyDown(KeyCode.R))
            {
                Player_Spawn_D();
                StartCoroutine(Pause(0.1f));
                time_d = cool_d;
            }
            buttonD.image.fillAmount = 1;
            BackGrounds[3].SetActive(true);
        } else
        {
            time_d -= Time.deltaTime;
            buttonD.image.fillAmount = 1 - time_d / cool_d;
            shot_d = false;
            BackGrounds[3].SetActive(false);
        }
    }

    // 버튼 클릭
    public void OnBtnClick_A()
    {
        if (Time.timeScale == 1)
            shot_a = true;
    }
    public void OnBtnClick_B()
    {
        if (Time.timeScale == 1)
            shot_b = true;
    }
    public void OnBtnClick_C()
    {
        if (Time.timeScale == 1)
            shot_c = true;
    }
    public void OnBtnClick_D()
    {
        if (Time.timeScale == 1)
            shot_d = true;
    }

    // 스킬 생성
    private void Player_Spawn_A()
    {
        Vector2 pos = Player.transform.position;
        pos.y += 1.5f;
        Instantiate(Player_Skill_A, pos, Quaternion.Euler(0, 0, angle));
    }
    private void Player_Spawn_B()
    {
        Vector2 pos = Player.transform.position;
        pos.y += 0.5f;
        Instantiate(Player_Skill_B, pos, Quaternion.Euler(0, 0, angle));
    }
    private void Player_Spawn_D()
    {
        Vector2 pos = Player.transform.position;
        pos.y += 1.5f;
        Instantiate(Player_Skill_D, pos, Quaternion.Euler(0, 0, angle));
    }
    IEnumerator Player_Flash()
    {
        GameObject afterImg = Instantiate(PlayerAfterImg, Player.transform.position, Quaternion.Euler(0, 0, 0));
        afterImg.transform.localScale = new Vector3(-0.22f * playerMove.dir, 0.22f, 1);
        Destroy(afterImg, 1.5f);
        StartCoroutine(Pause(0.1f));
        yield return new WaitForSeconds(0.1f);
        playerMove.Flash();
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

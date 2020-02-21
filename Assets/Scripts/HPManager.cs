using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    Image img_hp;
    float hp, speed;
    void Start()
    {
        if (img_hp == null)
            img_hp = gameObject.GetComponent<Image>();
        hp = 70f;
        speed = 5f;
    }

    void Update()
    {
        if (hp > 0)
            hp -= Time.deltaTime * speed;
        else
        {
            // 게임 오버
        }
        img_hp.fillAmount = hp / 100;
    }

    public void PlusHP(float d)
    {
        hp += d;
    }
}

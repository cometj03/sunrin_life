using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    Image HP_Gauge;
    Text HP_Text;
    float hp, speed;
    
    void Start()
    {
        if (HP_Gauge == null)
            HP_Gauge = gameObject.GetComponent<Image>();
        if (HP_Text == null)
            HP_Text = gameObject.GetComponentInChildren<Text>();
        hp = 6f;
        speed = 7f;
    }

    private void FixedUpdate()
    {
        HP_Gauge.fillAmount = hp / 100;
        speed = hp / 8;
        if (hp >= 100)
        {
            // Game Clear
        }else if (hp >= 0)
        {
            hp -= Time.deltaTime * speed;
        }

        HP_Text.text = hp.ToString("N0") + "%";
    }

    public void PlusHP(float d)
    {
        if (hp + d > 0)
            hp += d;
        else
        {
            // Game Over
        }
    }
}

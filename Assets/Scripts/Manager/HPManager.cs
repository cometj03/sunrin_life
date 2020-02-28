using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    Image HP_Gauge;
    Text HP_Text;
    float hp, speed;
    public static float difficulty;
    
    void Start()
    {
        if (HP_Gauge == null)
            HP_Gauge = gameObject.GetComponent<Image>();
        if (HP_Text == null)
            HP_Text = gameObject.GetComponentInChildren<Text>();
        hp = 20f;
        difficulty = 6f;
    }

    private void FixedUpdate()
    {
        HP_Gauge.fillAmount = hp / 100;
        speed = difficulty * hp / 100;
        if (hp >= 0)
        {
            hp -= Time.deltaTime * speed;
        }

        if (GameManager.instance.gameState == GameState.Progressing)
            HP_Text.text = hp.ToString("N0") + "%";
    }

    public void PlusHP(float d)
    {
        if (hp + d > 0)
        {
            if (hp >= 100)
            {
                GameManager.instance.gameState = GameState.Clear;
                HP_Text.text = "Game Clear!";
            }
            hp += d;
        }
        else
        {
            // Game Over
            GameManager.instance.gameState = GameState.Over;
            HP_Text.text = "GameOver!";
            GameManager.instance.GameOver();
        }
    }
}

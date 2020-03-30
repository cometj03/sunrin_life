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
        hp = 45f;
        difficulty = (int)DataManager.instance.currentStage * 2 + 3;
    }

    private void Update()
    {
        HP_Gauge.fillAmount = hp / 100;
        speed = difficulty * hp / 100;
        
        if (GameManager.instance.gameState == GameState.Progressing)
        {
            if (hp >= 0)
            {
                hp -= Time.deltaTime * speed;
            }
            HP_Text.text = hp.ToString("N0") + "%";
        }
    }

    public void PlusHP(float d)
    {
        if (hp + d > 0)
        {
            hp += d;
            if (hp >= 100)
            {
                // Game Clear
                HP_Text.color = Color.white;
                GameManager.instance.gameState = GameState.Clear;
                HP_Text.text = "100%\nGame Clear!";
                GameManager.instance.GameClear();
            }
        }
        else
        {
            // Game Over
            HP_Text.color = Color.white;
            GameManager.instance.gameState = GameState.Over;
            HP_Text.text = "0%\nGameOver!";
            GameManager.instance.GameOver();
        }
    }
}

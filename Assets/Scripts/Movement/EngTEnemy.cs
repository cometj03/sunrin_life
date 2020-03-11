using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngTEnemy : EnemyMove
{
    public GameObject Trump;
    float cool_Trump, coolTimer_Trump;

    private void Awake()
    {
        cool_Trump = Random.Range(4.0f, 8.0f);
        coolTimer_Trump = 0f;
    }

    void Update()
    {
        if (coolTimer_Trump < cool_Trump)
            coolTimer_Trump += Time.deltaTime;
        else if (GameManager.instance.gameState == GameState.Progressing)
        {
            Vector2 pos = transform.position;
            pos.y = 7f;

            Instantiate(Trump, pos, Quaternion.Euler(0, 0, 0));

            cool_Trump = Random.Range(4.0f, 8.0f);
            coolTimer_Trump = 0f;
        }
    }
}

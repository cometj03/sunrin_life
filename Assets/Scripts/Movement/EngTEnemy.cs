using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngTEnemy : EnemyMove
{
    public GameObject Trump;

    private void Start()
    {
        StartCoroutine(TrumpSpawn());
    }

    IEnumerator TrumpSpawn()
    {
        while (GameManager.instance.gameState == GameState.Progressing) {

            if (coolTimer_Trump < cool_Trump)
                coolTimer_Trump += Time.deltaTime;
            else if (GameManager.instance.gameState == GameState.Progressing)
            {
                Vector2 pos = transform.position;
                pos.y = 7f;

                Instantiate(Trump, pos, Quaternion.Euler(0, 0, 0));

                cool_Trump = Random.Range(5.5f, 9.5f);
                coolTimer_Trump = 0f;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject Enemy;

    void Start()
    {
        Enemy = Instantiate(EnemyPrefabs[(int)DataManager.instance.currentStage]);
        Enemy.transform.position = transform.position;
    }
}

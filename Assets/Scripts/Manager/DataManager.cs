using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentStage
{
    tutorial, teacher1, teacher2
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public CurrentStage currentStage;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }
}

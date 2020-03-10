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

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 화면 비율
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
    }
}

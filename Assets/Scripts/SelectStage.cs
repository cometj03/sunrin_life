using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    public CurrentStage currentStage;

    void Start()
    {
        
    }

    public void OnStageClick()
    {
        DataManager.instance.currentStage = currentStage;
        SceneManager.LoadScene("InGame");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    inGame,
    GameOver
}
public enum GameStage
{

}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gameState;   // 0: 게임 오버, 1: 진행중, 2: 게임 클리어
    public int level;
    public GameObject curtain;

    public float time, _fadeTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        //DontDestroyOnLoad(gameObject);
        time = 0f;
        _fadeTime = 1f;
        gameState = 1;
        
        //testText = GameObject.Find("testText").GetComponent<Text>();
        //testText.text = "level:" + level.ToString();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(FadeOut());
    }

    public void GameOver()
    {
        gameState = 0;
        StartCoroutine(FadeIn());
    }

    public void ClearGame()
    {

    }

    IEnumerator FadeOut()
    {
        while (time < _fadeTime)
        {
            curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f - time / _fadeTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        time = 0;
        yield return null;
    }

    IEnumerator FadeIn()
    {
        while (time < 1.5f)
        {
            curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, time / 1.5f);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        time = 0;
        yield return null;
    }

}

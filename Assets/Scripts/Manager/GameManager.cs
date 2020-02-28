using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Stage
{
    StartScene,
    inMenu
}
public enum GameState
{
    Progressing,
    Clear,
    Over
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;
    public Stage stageScene;
    public int level;

    GameObject curtain;
    public float time, _fadeTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        //DontDestroyOnLoad(gameObject);
        if (curtain == null)
            curtain = GameObject.Find("curtain");
        time = 0f;
        _fadeTime = 1f;
        gameState = GameState.Progressing;
        stageScene = Stage.StartScene;
    }

    private void Start()
    {

    }

    public void StartGame()
    {
        StartCoroutine(FadeOut());
    }

    public void GameOver()
    {
        gameState = GameState.Over;
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

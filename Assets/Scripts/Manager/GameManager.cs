using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject curtain;
    public float time, _fadeTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        //DontDestroyOnLoad(gameObject);
        time = 0f;
        _fadeTime = 1f;
        gameState = GameState.Progressing;
        stageScene = Stage.StartScene;
    }

    private void Start()
    {
        /*GameObject[] gm = GameObject.FindGameObjectsWithTag("GameManager");
        for (int i = 1; gm[i] != null; i++)
            Destroy(gm[i]);*/
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

    public void GameClear()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        if (curtain == null)
            curtain = GameObject.Find("curtain");
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
        if (curtain == null)
            curtain = GameObject.Find("curtain");
        while (time < 1.5f)
        {
            curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, time / 1.5f);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        curtain.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        time = 0;
        SceneManager.LoadScene("StageScene");
        yield return null;
    }
}

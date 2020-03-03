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
    NotStarted,
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
        stageScene = Stage.StartScene;
    }

    private void Start()
    {
        /*GameObject[] gm = GameObject.FindGameObjectsWithTag("GameManager");
        for (int i = 1; gm[i] != null; i++)
            Destroy(gm[i]);*/
        if (SceneManager.GetActiveScene().name == "InGame")
            gameState = GameState.Progressing;
    }

    public void _FadeOut()
    {
        StartCoroutine(FadeOut());
    }
    public void _FadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void GameOver()
    {
        gameState = GameState.Over;
        _FadeIn();
    }

    public void GameClear()
    {
        gameState = GameState.Clear;
        _FadeIn();
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
        while (time < 1.25f)
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

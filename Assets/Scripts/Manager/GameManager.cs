using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CurruntScene
{
    StageScene,
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
    public CurruntScene stageScene;

    public GameObject curtain;
    public float time, _fadeTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        time = 0f;
        _fadeTime = 1f;
        stageScene = CurruntScene.StageScene;
    }

    private void Start()
    {
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
        _FadeIn();
    }

    public void GameClear()
    {
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

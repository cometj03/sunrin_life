using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] stages;
    public GameObject StartPanel, AlertPanel;

    private int numStage, _numStage;
    private bool openedPanel;

    void Awake()
    {
        numStage = 0;
        _numStage = 3;
        openedPanel = false;
    }

    private void Start()
    {
        GameManager.instance.stageScene = Stage.StartScene;

        for (int i = 0; i < 4; i++)
            stages[i].SetActive(false);
        stages[numStage].SetActive(true);
    }

    void Update()
    {
        if (GameManager.instance.stageScene == Stage.StartScene)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                StartPanel.SetActive(false);
                AlertPanel.SetActive(false);
                GameManager.instance.StartGame();
                GameManager.instance.stageScene = Stage.inMenu;
            }
            else if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
            {
                GameQuit();
            }
        }
        else if (GameManager.instance.stageScene == Stage.inMenu)
        {
            if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
            {
                if (openedPanel)
                    CloseAlertPanel();
                else
                    OpenAlertPanel();
                openedPanel = openedPanel ? false : true;
            }
        }
    }

    private void Show(int n)    // 프로필 보여주기
    {
        for (int i = 0; i < 4; i++)
            stages[i].SetActive(false);
        stages[numStage].SetActive(true);
        stages[_numStage].SetActive(true);

        // 포지션 초기화
        stages[numStage].transform.position = new Vector2(17 * n, 0);
    }

    public void OnClickNext()   // 다음 화살표 누름
    {
        _numStage = numStage;
        if (numStage < 3)
            numStage++;
        else
            numStage = 0;
        Show(1);
        StartCoroutine(StageLeave(1));
        StartCoroutine(StageEnter(1));
    }
    public void OnClickPre()    // 이전 화살표 누름
    {
        _numStage = numStage;
        if (numStage > 0)
            numStage--;
        else
            numStage = 3;
        Show(-1);
        StartCoroutine(StageLeave(-1));
        StartCoroutine(StageEnter(-1));
    }

    IEnumerator StageEnter(int tmp) // 스테이지 입장
    {
        Vector3 pos = stages[numStage].transform.position;
        Vector3 target = Vector3.zero;
        float time = 0;
        
        while (time < 2f)
        {
            stages[numStage].transform.position = pos;
            pos = Vector3.Lerp(pos, target, Time.deltaTime * (time + 1.5f));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    IEnumerator StageLeave(int tmp) // 스테이지 퇴장
    {
        //Vector3 pos = Vector3.zero;
        Vector3 pos = stages[_numStage].transform.position;
        Vector3 target = Vector3.zero;
        target.x = -17 * tmp;
        float time = 0;

        while (time < 1.5f)
        {
            stages[_numStage].transform.position = pos;
            pos = Vector3.Lerp(pos, target, Time.deltaTime * (time + 1.5f));
            //stages[_numStage].transform.position = Vector3.Lerp(stages[_numStage].transform.position, target, Time.deltaTime * (time + 1));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void OpenAlertPanel()    // 알림창 열기
    {
        AlertPanel.SetActive(true);
        openedPanel = true;
    }
    public void CloseAlertPanel()   // 알림창 닫기
    {
        AlertPanel.SetActive(false);
        openedPanel = false;
    }
    public void GotoStart() // 초기화면으로 돌아가기
    {
        StartPanel.SetActive(true);
        GameManager.instance.stageScene = Stage.StartScene;
    }
    public void GameQuit()  // 게임 종료
    {
        Application.Quit();
    }

    public void GotoInGame()
    {
        SceneManager.LoadScene("InGame");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    struct Stages
    {
        public GameObject stage; // { get; set; }
        public Image[] image;
        public Image title;
        public Image name;
    }
    Stages[] stages = new Stages[4];

    /*GameObject Stage0;
    GameObject Stage1;
    GameObject Stage2;
    GameObject Stage3;*/

    GameObject MainPanel;
    GameObject AlertPanel;

    public bool isMain;

    Vector2 zero = Vector2.zero;

    int S_Count;

    void Awake()
    {
        isMain = true;
        this.MainPanel = GameObject.Find("MainPanel");

        this.AlertPanel = GameObject.Find("AlertPanel");
        AlertPanel.SetActive(false);

        S_Count = 0;

        for (int i = 0; i < 3; i++)
        {
            stages[i].stage = GameObject.Find("Stage" + i.ToString());
            stages[i].image = new Image[3];

            stages[i].image[0] = stages[i].stage.transform.Find("Portrait").gameObject.GetComponent<Image>();
            stages[i].image[1] = stages[i].stage.transform.Find("Title").gameObject.GetComponent<Image>();
            stages[i].image[2] = stages[i].stage.transform.Find("Name").gameObject.GetComponent<Image>();
        }
        stages[3].stage = GameObject.Find("Stage3");
        stages[3].image = new Image[3];
        stages[3].image[0] = stages[3].stage.transform.Find("Coming").gameObject.GetComponent<Image>();

        Show(1);
    }

    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && isMain)
        {
            Debug.Log("a");
            MainPanel.SetActive(false);
            isMain = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && Application.platform == RuntimePlatform.Android)
        {
            OpenPanel();
        }
    }

    public void OnClick_Next()
    {
        if (S_Count < 3)
            S_Count++;
        else
            S_Count = 0;
        GameManager.instance.level = S_Count;
        Debug.Log("OnClick_Next");
        Show(1);
    }

    public void OnClick_Pre()
    {
        if (S_Count > 0)
            S_Count--;
        else
            S_Count = 3;
        GameManager.instance.level = S_Count;
        Debug.Log("OnClick_Pre");
        Show(-1);
    }

    void Show(int a)
    {
        /*for (int i = 0; i < 3; i++)
            if (stages[S_Count].image[i] != null)
                stages[S_Count].image[i].fillAmount = 0;
            else
                Debug.Log(S_Count.ToString() + "stage " + i.ToString() + " image is missing!");
        */
        int S_other = S_Count - a < 0 ? 3 : S_Count - a;
        S_other = S_Count - a >= 4 ? 0 : S_other;
        Debug.Log("other Stage is " + S_other.ToString());
        //Debug.Log("stage : " + S_Count.ToString() + ", direction is " + a.ToString());
        Hide();
        Vector2 pos = new Vector2(a * 900, 0);
        stages[S_Count].stage.transform.position = pos;
        stages[S_Count].stage.SetActive(true);
        /*float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            Vector2 pos2 = Vector2.Lerp(stages[S_Count].stage.transform.position, zero, t);
            stages[S_Count].stage.transform.position = pos2;
        }*/

        //Debug.Log("Show()");
        //Slide(a);
    }

    /*void Slide(int a)
    {
        Debug.Log("Slide()");
        float leftTime = 10.0f, coolTime = 10.0f;
        if (a > 0)
        {
            for (int i = 0; i < 3; i++)
                if (stages[S_Count].image[i] != null)
                    stages[S_Count].image[i].fillOrigin = (int)Image.OriginHorizontal.Right;
            while (leftTime > 0)
            {
                leftTime -= Time.deltaTime;
                
                for (int i = 0; i < 3; i++)
                    if (stages[S_Count].image[i] != null)
                    {
                        float ratio = leftTime / coolTime;
                        stages[S_Count].image[0].fillAmount = ratio;
                    }
                        
            }
        }
        else if (a < 0)
        {
            for (int i = 0; i < 3; i++)
                if (stages[S_Count].image[i] != null)
                    stages[S_Count].image[i].fillOrigin = (int)Image.OriginHorizontal.Left;
        }
    }*/

void Hide()
    {
        for (int i = 0; i < 4; i++)
            stages[i].stage.SetActive(false);
        /*Stage0.SetActive(false);
        Stage1.SetActive(false);
        Stage2.SetActive(false);
        Stage3.SetActive(false);*/
    }

    public void GoHome()
    {
        MainPanel.SetActive(true);
        AlertPanel.SetActive(false);
        isMain = true;
    }

    public void onClick_Exit()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        AlertPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        AlertPanel.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}

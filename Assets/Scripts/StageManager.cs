using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene currScene = SceneManager.GetActiveScene();
        string sceneName = currScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stage1()
    {
        SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);
    }
    public void stage2()
    {
        SceneManager.LoadScene("SecondStage", LoadSceneMode.Single);
    }
    public void stage3()
    {
        SceneManager.LoadScene("ThirdStage", LoadSceneMode.Single);
    }
    public void stage4()
    {
        SceneManager.LoadScene("FourthStage", LoadSceneMode.Single);
    }
    public void stage5()
    {
        SceneManager.LoadScene("FifthStage", LoadSceneMode.Single);
    }
    public void stage6()
    {
        SceneManager.LoadScene("SixthStage", LoadSceneMode.Single);
    }
    public void stage7()
    {
        SceneManager.LoadScene("SeventhStage", LoadSceneMode.Single);
    }
    public void stage8()
    {
        SceneManager.LoadScene("EighthStage", LoadSceneMode.Single);
    }
    public void stage9()
    {
        SceneManager.LoadScene("NinthStage", LoadSceneMode.Single);
    }
    public void stage10()
    {
        SceneManager.LoadScene("TenthStage", LoadSceneMode.Single);
    }
    public void stage11()
    {
        SceneManager.LoadScene("EleventhStage", LoadSceneMode.Single);
    }
    public void stage12()
    {
        SceneManager.LoadScene("TwelfthStage", LoadSceneMode.Single);
    }
    public void stage13()
    {
        SceneManager.LoadScene("ThirteenthStage", LoadSceneMode.Single);
    }
    public void stage14()
    {
        SceneManager.LoadScene("Fourteenthtage", LoadSceneMode.Single);
    }
    public void stage15()
    {
        SceneManager.LoadScene("FifteenthStage", LoadSceneMode.Single);
    }
}

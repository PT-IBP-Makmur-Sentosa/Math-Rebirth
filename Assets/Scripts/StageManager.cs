using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    GameObject glob;
    GlobalControl globalcontrol;
    // Start is called before the first frame update
    void Start()
    {
        Scene currScene = SceneManager.GetActiveScene();
        string sceneName = currScene.name;
        glob = GameObject.Find("GlobalObject");
        globalcontrol = glob.GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stage1()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage2()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("SecondStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage3()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("ThirdStage", LoadSceneMode.Single);
    }
    public void stage4()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FourthStage", LoadSceneMode.Single);
    }
    public void stage5()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FifthStage", LoadSceneMode.Single);
    }
    public void stage6()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("SixthStage", LoadSceneMode.Single);
    }
    public void stage7()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("SeventhStage", LoadSceneMode.Single);
    }
    public void stage8()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("EighthStage", LoadSceneMode.Single);
    }
    public void stage9()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("NinthStage", LoadSceneMode.Single);
    }
    public void stage10()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("TenthStage", LoadSceneMode.Single);
    }
    public void stage11()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("EleventhStage", LoadSceneMode.Single);
    }
    public void stage12()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("TwelfthStage", LoadSceneMode.Single);
    }
    public void stage13()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("ThirteenthStage", LoadSceneMode.Single);
    }
    public void stage14()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("Fourteenthtage", LoadSceneMode.Single);
    }
    public void stage15()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FifteenthStage", LoadSceneMode.Single);
    }
}

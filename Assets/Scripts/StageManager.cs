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
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage4()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FourthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage5()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FifthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage6()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("SixthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage7()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("SeventhStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage8()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("EighthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage9()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("NinthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage10()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("TenthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage11()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("EleventhStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage12()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("TwelfthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage13()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("ThirteenthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage14()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("Fourteenthtage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void stage15()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("FifteenthStage", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
    public void shop()
    {
        globalcontrol.SaveGame();
        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
        globalcontrol.LoadGame();
        print("load");
    }
}

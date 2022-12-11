using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject map;
    public GameObject checkpoint_map1;
    public GameObject checkpoint_map2;
    public GameObject player;
    public GameObject checkpoint_1;
    public GameObject checkpoint_2;
    public GameObject finish_map_1;
    GameObject glob;
    GlobalControl globalcontrol;
    private int bool_map_1;
    private int bool_map_2;
    public GameObject button_map_1;
    public GameObject button_map_2;
    List<int> stagelist = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        glob = GameObject.Find("GlobalObject");
        globalcontrol = glob.GetComponent<GlobalControl>();
        foreach(int x in globalcontrol.stageList)
        {
            stagelist.Add(x);
        }
        bool_map_1 = stagelist[0];
        bool_map_2 = stagelist[1];
        if(bool_map_1 == 1)
        {
            button_map_1.SetActive(true);
        }
        if(bool_map_2 == 1)
        {
            button_map_2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void map1()
    {
        Scene currScene = SceneManager.GetActiveScene();
        string sceneName = currScene.name;
        if(sceneName == "FirstStage")
        {
            checkpoint_map2.SetActive(false); 
            if(bool_map_1 == 1)
            {
               
            }
            checkpoint_map1.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);
        }
        
    }
    public void map2()
    {
        Scene currScene = SceneManager.GetActiveScene();
        string sceneName = currScene.name;
        if(sceneName == "SecondStage")
        {
            checkpoint_map2.SetActive(false);    
            checkpoint_map1.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("SecondStage", LoadSceneMode.Single);
        }
    }
    public void check1()
    {
        checkpoint_map1.SetActive(false);
        checkpoint_map2.SetActive(false);
        map.SetActive(false);
        player.GetComponent<Transform>().position = checkpoint_1.transform.position;
        player.GetComponent<Transform>().position = new Vector3( player.GetComponent<Transform>().position.x + 1.2f,  player.GetComponent<Transform>().position.y,  player.GetComponent<Transform>().position.z);
    }
    public void check2()
    {
        checkpoint_map1.SetActive(false);
        checkpoint_map2.SetActive(false);
        map.SetActive(false);
        player.GetComponent<Transform>().position = checkpoint_2.transform.position;
        player.GetComponent<Transform>().position = new Vector3( player.GetComponent<Transform>().position.x - 1.2f,  player.GetComponent<Transform>().position.y,  player.GetComponent<Transform>().position.z);
        
    }
}

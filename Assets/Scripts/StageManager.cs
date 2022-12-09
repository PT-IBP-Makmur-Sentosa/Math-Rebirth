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


    // Start is called before the first frame update
    void Start()
    {
        
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

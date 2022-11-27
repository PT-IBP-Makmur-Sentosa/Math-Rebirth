using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class Mainmenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject help;
    public GameObject help1;
    public GameObject help2;
    public GameObject setting;
    public GameObject about;
    // public Canvas menuScreens;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame() {  

    }  
    public void QuitGame() {  
        Application.Quit(); 
    }  
    public void About() {  
        about.SetActive(true);
        menu.SetActive(false);
    }  
    public void Setting() {  
        setting.SetActive(true);
        menu.SetActive(false);
    } 
    public void Help() {  
        help.SetActive(true);
        menu.SetActive(false);
    }   
    public void Help1() {  
        help1.SetActive(true);
        help.SetActive(false);
    }  
    public void Help2() {  
        help2.SetActive(true);
        help1.SetActive(false);
    }  public void backmenu() {  
        menu.SetActive(true);
        help.SetActive(false);
        help1.SetActive(false);
        help2.SetActive(false);
        about.SetActive(false);
        setting.SetActive(false);

    }  
}


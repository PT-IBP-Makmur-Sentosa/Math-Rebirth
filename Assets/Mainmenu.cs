using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Mainmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene("About"); 
    }  
    public void Setting() {  
        SceneManager.LoadScene("Setting"); 
    } 
    public void Help() {  
        SceneManager.LoadScene("Help"); 
    }   
}


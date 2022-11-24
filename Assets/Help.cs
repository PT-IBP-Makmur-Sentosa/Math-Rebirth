using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadNextScene(){
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }

    public void LoadPreviousScene(){
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current-1);
    }
    
    // Update is called once per frame
    public void LoadStartScene(){
        SceneManager.LoadScene(0);
    }
    public void Loadmenu(){
        SceneManager.LoadScene("Mainmenu");
    }
}

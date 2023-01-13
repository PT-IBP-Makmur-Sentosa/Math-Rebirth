using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public float fadeSpeed;
    public string MainMenu;
    private bool shouldFadeToBlack;
    public GameObject fadeScreen;

    void Start()
    {
        fadeScreen.SetActive(false);
    }

    void Update()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.GetComponent<Image>().color = new Color(fadeScreen.GetComponent<Image>().color.r, fadeScreen.GetComponent<Image>().color.g, fadeScreen.GetComponent<Image>().color.b, Mathf.MoveTowards(fadeScreen.GetComponent<Image>().color.a, 1.0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.GetComponent<Image>().color.a == 1.0f)
            {
                shouldFadeToBlack = false;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        fadeScreen.SetActive(true);
        StartCoroutine(eFadeOut());
    }

    void Fadeout()
    {
        shouldFadeToBlack = true;
    }

    private IEnumerator eFadeOut()
    {
        yield return new WaitForSeconds(1.0f / fadeSpeed);
        Fadeout();

        yield return new WaitForSeconds((1.0f / fadeSpeed));
        SceneManager.LoadScene(MainMenu);
    }
}

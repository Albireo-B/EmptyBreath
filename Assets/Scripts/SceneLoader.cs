using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jouer()
    {
        SceneManager.LoadScene("EmptyBreath");

    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quitter()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}

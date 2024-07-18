using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void endGame()
    {
        SceneManager.LoadScene("died");
    }
    public void returnToTheMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    public GameObject XWon;
    public GameObject OWon;
    public GameObject NoOneWon;
    public GameObject RestartButton;

    public void XWonEnd()
    {
        XWon.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void OWonEnd()
    {
        OWon.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void NoOneWonEnd()
    {
        NoOneWon.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

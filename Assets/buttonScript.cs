using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public Slider boardSize;
    public Toggle hardmode;
    public GameObject UI;
    public GameObject gridPrefab;
    public void startGame()
    {
        GameObject newGame = Instantiate(gridPrefab);
        gridManager newGrid = newGame.GetComponent<gridManager>();
        newGrid.gridSize = (int)boardSize.value;
        newGame.GetComponentInChildren<noughtsNCrossesAI>().hardMode = hardmode.isOn;
        UI.SetActive(false);
    }
}

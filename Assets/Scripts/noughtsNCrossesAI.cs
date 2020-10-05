using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noughtsNCrossesAI : MonoBehaviour
{
    public gridManager grid;
    public float timeToWait;
    public float thinkTime = 5;
    public bool MyTurn = false;
    public bool gaveUp = false;
    public int maxTriesTilGiveUp = 20;
    public int currentTries = 0;
    public bool hardMode;
    // Start is called before the first frame update
    void Start()
    {
        timeToWait = thinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hardMode)
        {

            if (grid.gameOver)
            {
                this.enabled = false;
            }

            if (MyTurn && !gaveUp)
            {
                timeToWait -= Time.deltaTime;
                if (timeToWait <= 0 && !gaveUp)
                {
                    MyTurn = false;
                    timeToWait = thinkTime + Random.Range(-2, 2);
                    makeMove();
                }
            }

            if (gaveUp)
            {
                makeRandomMove();
                currentTries = 0;
                gaveUp = false;
            }
        }

        else if (hardMode)
        {
            if (grid.gameOver)
            {
                this.enabled = false;
            }

            if (MyTurn)
            {
                timeToWait -= Time.deltaTime;
                if (timeToWait <= 0)
                {
                    MyTurn = false;
                    timeToWait = thinkTime + Random.Range(-2, 2);
                    makeHardMove();
                    
                }
            }
        }
    }

    public void makeMove()
    {
        if (currentTries >= maxTriesTilGiveUp)
        {
            Debug.LogError("i give up");
            gaveUp = true;
        }
        if (gaveUp)
        {
            return;
        }
        Vector2 gridPos = getGridPos();
        if (gridPos == new Vector2(999,999))
        {
            return;
        }
        GameObject gridObj = grid.gridTiles[System.Array.IndexOf(grid.gridPositions, gridPos)];
        clickDetection gridPosClick = gridObj.GetComponent<clickDetection>();
        if (gridPosClick.isAnO || gridPosClick.isAnX)
        {
            if (!gaveUp)
            {
                Debug.Log("try again, dipshit");
                currentTries += 1;
                makeMove();
            }
        }
        else
        {
            grid.gridTiles[System.Array.IndexOf(grid.gridPositions, gridPos)].GetComponent<clickDetection>().placeO();
        }
    }

    public void makeHardMove()
    {
        int oppositeIndexNumber = (grid.gridPositions.Length - 1) - grid.lastXPlaced.GetComponent<clickDetection>().indexInArray;
        clickDetection hardDecision = grid.gridTiles[oppositeIndexNumber].GetComponent<clickDetection>();
        if (hardDecision.isAnO || hardDecision.isAnX)
        {
            makeRandomMove();
            return;
        }
        else
        {
            hardDecision.placeO();
        }
    }

    public void makeRandomMove()
    {
        clickDetection randomPosClick = grid.gridTiles[Random.Range(0, grid.gridTiles.Length - 1)].GetComponent<clickDetection>();
        if (randomPosClick.isAnO || randomPosClick.isAnX)
        {
            makeRandomMove();
            return;
        }
        else
        {
            randomPosClick.placeO();
        }
    }

    public Vector2 getGridPos()
    {
        if (currentTries >= maxTriesTilGiveUp)
        {
            gaveUp = true;
        }
        Vector2 newGridPos;
        newGridPos = grid.gridPositions[System.Array.IndexOf(grid.gridTiles, grid.lastXPlaced)] + new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        if (gaveUp)
        {
            return new Vector2(999,999);
        }
        if (newGridPos.x < 0)
        {
            currentTries += 1;
            newGridPos = getGridPos();
            return newGridPos;
        }

        if (newGridPos.y < 0)
        {
            currentTries += 1;
            newGridPos = getGridPos();
            return newGridPos;
        }

        if (newGridPos.x >= grid.gridSize || newGridPos.y >=grid.gridSize)
        {
            currentTries += 1;
            newGridPos = getGridPos();
            return newGridPos;
        }

        if (newGridPos == (Vector2)grid.lastXPlaced.transform.position)
        {
            currentTries += 1;
            newGridPos = getGridPos();
            return newGridPos;
        }
        return newGridPos;
    }
}

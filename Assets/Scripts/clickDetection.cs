using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickDetection : MonoBehaviour
{
    public Vector2 tilePositionToGrid;
    public int indexInArray;
    public bool wasClicked;
    public bool isAnX;
    public bool isAnO;
    public keepTrackOfTurn keepTrack;
    public gridManager grid;
    public GameObject X;
    public GameObject O;
    
    void Start()
    {
        grid = transform.parent.GetComponent<gridManager>();
        keepTrack = transform.parent.GetComponent<keepTrackOfTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !wasClicked && !grid.gameOver && keepTrack.xTurn)
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit2D = Physics2D.Raycast(rayPos, Vector2.zero, 0.01f);
            Debug.DrawLine(rayPos, Vector2.zero);
            if (hit2D.collider.gameObject == this.gameObject)
            {
                wasClicked = true;
                if (keepTrack.xTurn)
                {
                    placeX();
                }
                grid.checkForWin(indexInArray);
                keepTrack.swapTurn();
            }
        }

        if (wasClicked && !grid.gameOver)
        {
            grid.checkForWin(indexInArray);
        }
    }

    public void placeX()
    {
        GameObject lastX = Instantiate(X, transform.position, X.transform.rotation, transform);
        isAnX = true;
        grid.lastXPlaced = gameObject;
        grid.AddTry();
    }

    public void placeO()
    {
        GameObject lastO = Instantiate(O, transform.position, Quaternion.identity, transform);
        isAnO = true;
        wasClicked = true;
        grid.lastOPlaced = lastO;
        grid.checkForWin(indexInArray);
        keepTrack.swapTurn();
        grid.AddTry();
    }
}

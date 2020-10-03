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
    
    void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<gridManager>();
        keepTrack = GameObject.Find("gameManager").GetComponent<keepTrackOfTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !wasClicked)
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit2D = Physics2D.Raycast(rayPos, Vector2.zero, 0.01f);
            Debug.DrawLine(rayPos, Vector2.zero);
            if (hit2D.collider.gameObject == this.gameObject)
            {
                wasClicked = true;
                if (keepTrack.xTurn)
                {
                    Instantiate(X, transform.position, X.transform.rotation);
                    isAnX = true;
                }
                else
                {
                    Instantiate(O,transform.position, Quaternion.identity);
                    isAnO = true;
                }
                grid.checkForWin(indexInArray);
                keepTrack.swapTurn();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && wasClicked)
        {
            grid.checkForWin(indexInArray);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    [Range(3,100)]
    public int gridSize;
    [Range(0,10)]
    public float gridSpacing;
    public Vector2[] gridPositions;
    public Vector2[] worldPositions;
    public Vector3 clickDetectorPredeterminedScale;
    public bool gameOver = false;
    public GameObject[] gridTiles;
    [Space]
    public int MovesMade = 0;
    public GameObject ClickDetector;
    public GameObject lastXPlaced;
    public GameObject lastOPlaced;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
        gridPositions = new Vector2[gridSize * gridSize];
        worldPositions = new Vector2[gridSize * gridSize];
        gridTiles = new GameObject[gridSize * gridSize];
        initiateGrid(gridSize);
        DetermineCameraSize();
    }
    public void initiateGrid(int sizeOfGrid)
    {
        for (int x = 0; x < sizeOfGrid; x++)
        {
            for(int y = 0; y < sizeOfGrid; y++)
            {
                int arrayIndex = sizeOfGrid * x + y;
                gridPositions[arrayIndex] = new Vector2(x, y);
                worldPositions[arrayIndex] = new Vector2(x + x * gridSpacing, y + y * gridSpacing);
                GameObject newClickDetector = Instantiate(ClickDetector, worldPositions[arrayIndex], Quaternion.identity, transform);
                newClickDetector.name = arrayIndex.ToString();
                gridTiles[arrayIndex] = newClickDetector;
                clickDetection M_clickDetection = newClickDetector.GetComponent<clickDetection>();
                M_clickDetection.indexInArray = arrayIndex;
                M_clickDetection.tilePositionToGrid = gridPositions[arrayIndex];
            }
        }
    }

    public void checkForWin(int indexInManagerArrays)
    {
        for (int i = 0; i <= 7; i++)
        {
            try
            {
                switch (i)
                {
                case 0:
                    checkWinInDirection(gridPositions[indexInManagerArrays],Vector2.up);
                    break;
                case 1:
                    checkWinInDirection(gridPositions[indexInManagerArrays], Vector2.down);
                    break;
                case 2:
                    checkWinInDirection(gridPositions[indexInManagerArrays], Vector2.right);
                    break;
                case 3:
                    checkWinInDirection(gridPositions[indexInManagerArrays], Vector2.left);
                    break;
                case 4:
                    checkWinInDirection(gridPositions[indexInManagerArrays], new Vector2(1,1));
                    break;
                case 5:
                    checkWinInDirection(gridPositions[indexInManagerArrays], new Vector2(-1, 1));
                    break;
                case 6:
                    checkWinInDirection(gridPositions[indexInManagerArrays], new Vector2(1, -1));
                    break;
                case 7:
                    checkWinInDirection(gridPositions[indexInManagerArrays], new Vector2(-1, -1));
                    break;
                default:
                    break;
                }

            }
            catch { };
        }
    }

    public void checkWinInDirection(Vector2 startPos ,Vector2 direction)
    {
        int originalObjectIndex;
        bool originalXBool = false;
        bool originalOBool = false;
        for (int v = 0; v < gridSize; v++)
        {
            if (v == 0)
            {
                originalObjectIndex = System.Array.IndexOf(gridPositions, startPos + direction * v);
                if(gridTiles[originalObjectIndex].GetComponent<clickDetection>().isAnX)
                {
                    originalXBool = true;
                }
                else if (gridTiles[originalObjectIndex].GetComponent<clickDetection>().isAnO)
                {
                    originalOBool = true;
                }
            }
            int objectIndex = System.Array.IndexOf(gridPositions,startPos + direction * v);
            if (originalXBool)
            {
                if (!gridTiles[objectIndex].GetComponent<clickDetection>().isAnX)
                {
                    originalXBool = false;
                    break;
                }
                if (v == gridSize-1)
                {
                    Debug.LogWarning("X Won");
                    gameOver = true;
                    GameObject.Find("Canvas").GetComponent<endGame>().XWonEnd();
                }
            }   
            if (originalOBool)
            {
                if (!gridTiles[objectIndex].GetComponent<clickDetection>().isAnO)
                {
                    originalOBool = false;
                    break;
                }
                if (v == gridSize - 1)
                {
                    Debug.LogWarning("O Won");
                    gameOver = true;
                    GameObject.Find("Canvas").GetComponent<endGame>().OWonEnd();
                }
            }
        }
    }

    public void AddTry()
    {
        MovesMade += 1;
        if (MovesMade >= gridSize * gridSize)
        {
            gameOver = true;
            GameObject.Find("Canvas").GetComponent<endGame>().NoOneWonEnd();
        }
    }
    
    public void DetermineCameraSize()
    {
        float floatGridSize = gridSize;
        float cameraDist = worldPositions[worldPositions.Length - 1].x / 2;
        Debug.Log(cameraDist);
        Camera.main.orthographicSize = (floatGridSize -0.75f) * gridSpacing ;
        Camera.main.transform.position = new Vector3(worldPositions[0].x + cameraDist, worldPositions[0].y + cameraDist, -1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public Grid grid;
    public GameObject X;
    public GameObject O;
    public int boardSize;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int v = 0; v < boardSize; v++)
            {
                float iPos = i * grid.cellSize.x + i * grid.cellGap.x;
                float vPos = v * grid.cellSize.y + v * grid.cellGap.y;
                Vector3 newPos = new Vector3(iPos, vPos);
                Vector3Int cellPos = grid.WorldToCell(newPos);
                Vector3 pos = grid.CellToWorld(cellPos);
                GameObject toSpawn;
                if (Random.Range(0, 2) == 0)
                {
                    toSpawn = X;
                }
                else
                {
                    toSpawn = O;
                }
                Instantiate(toSpawn, pos,toSpawn.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

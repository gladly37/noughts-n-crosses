using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepTrackOfTurn : MonoBehaviour
{
    public bool xTurn = true;
    public noughtsNCrossesAI AI;
    public void swapTurn()
    {
        if (xTurn)
        {
            AI.MyTurn = true;
        }
        xTurn = !xTurn;
    }
}

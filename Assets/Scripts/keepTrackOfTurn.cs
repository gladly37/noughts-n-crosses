using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepTrackOfTurn : MonoBehaviour
{
    public bool xTurn = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void swapTurn()
    {
        xTurn = !xTurn;

    }
}

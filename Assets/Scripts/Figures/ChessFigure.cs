using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessFigure : MonoBehaviour
{
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool PossibleMove(int x, int y)
    {
        return true;
    }
}

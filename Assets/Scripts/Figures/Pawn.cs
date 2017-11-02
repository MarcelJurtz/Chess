using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessFigure
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessFigure c, c2;

        if (isWhite)
        {
            // Diagonal Left
            if(CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX -1, CurrentY +1];
                if(c != null && !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Forward
            if(CurrentY != 7)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY + 1];
                if(c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }
            // Two Steps Forward
            if(CurrentY == 1)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY + 2];
                if(c == null && c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        }
        else
        {
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Forward
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }
            // Two Steps Forward
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.ChessFigurePositions[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }
        }

        return r;
    }
}

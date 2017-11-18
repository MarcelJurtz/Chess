using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessFigure
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        ChessFigure c;
        int i, j;

        // From Rook

        // Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = BoardManager.Instance.ChessFigurePositions[i, CurrentY];
            if (c == null) r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, CurrentY] = true;
                break;
            }
        }

        // Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.ChessFigurePositions[i, CurrentY];
            if (c == null) r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, CurrentY] = true;
                }
                break;
            }
        }

        // Forward
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8) break;
            c = BoardManager.Instance.ChessFigurePositions[CurrentX, i];
            if (c == null) r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }

        // Back
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = BoardManager.Instance.ChessFigurePositions[CurrentX, i];
            if (c == null) r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }

        // From Bishop

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 8) break;
            c = BoardManager.Instance.ChessFigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8) break;
            c = BoardManager.Instance.ChessFigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Bottom Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0) break;
            c = BoardManager.Instance.ChessFigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Bottom Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0) break;
            c = BoardManager.Instance.ChessFigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        return r;
    }
}

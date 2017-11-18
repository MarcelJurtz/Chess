using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour {

    System.Random r;
	public Vector2 MakeMove(ChessFigure figure)
    {
        r = new System.Random();
        
        bool[,] possibleMoves = figure.PossibleMove();

        List<Vector2> possibleMovements = new List<Vector2>();

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(possibleMoves[i,j])
                {
                    possibleMovements.Add(new Vector2(i, j));
                }
            }
        }

        if (possibleMovements.Count > 0) return possibleMovements[r.Next(possibleMovements.Count)];
        else return new Vector2(-1, -1);
    }

    public ChessFigure SelectChessFigure()
    {
        r = new System.Random();
        List<GameObject> activeFigures = BoardManager.Instance.GetAllActiveFigures();
        GameObject gameObject = activeFigures[r.Next(activeFigures.Count)];
        return gameObject.GetComponent<ChessFigure>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; set; }
    private bool[,] allowedMoves { get; set; }

    public ChessFigure[,] ChessFigurePositions { get; set; }
    private ChessFigure selectedFigure;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;
    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessFigures;
    private List<GameObject> activeFigures = new List<GameObject>();

    public bool isWhiteTurn = true;

    void Start()
    {
        Instance = this;
        ChessFigurePositions = new ChessFigure[8, 8];

        SpawnAllChessFigures();
    }

    void Update()
    {
        DrawChessBoard();
        UpdateSelection();

        if(Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedFigure == null)
                {
                    // Select Figure
                    SelectChessFigure(selectionX, selectionY);
                }
                else
                {
                    // Move Figure
                    MoveChessFigure(selectionX, selectionY);
                }
            }
        }
    }

    private void SelectChessFigure(int x, int y)
    {
        if (ChessFigurePositions[x, y] == null) return;
        if (ChessFigurePositions[x, y].isWhite != isWhiteTurn) return;

        bool hasAtLeastOneMove = false;
        allowedMoves = ChessFigurePositions[x, y].PossibleMove();

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(allowedMoves[i,j])
                {
                    hasAtLeastOneMove = true;

                    // break outer loop
                    i = 7;

                    // break inner loop
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove) return;

        selectedFigure = ChessFigurePositions[x, y];
        BoardHighlighting.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MoveChessFigure(int x, int y)
    {
        if(allowedMoves[x,y])
        {
            ChessFigure c = ChessFigurePositions[x, y];
            if(c != null && c.isWhite != isWhiteTurn)
            {
                activeFigures.Remove(c.gameObject);
                Destroy(c.gameObject);

                if(c.GetType() == typeof(King))
                {
                    EndGame();
                    return;
                }
            }

            ChessFigurePositions[selectedFigure.CurrentX, selectedFigure.CurrentY] = null;
            selectedFigure.transform.position = GetTileCenter(x, y);
            selectedFigure.SetPosition(x, y);
            ChessFigurePositions[x, y] = selectedFigure;
            isWhiteTurn = !isWhiteTurn;
        }

        BoardHighlighting.Instance.HideHighlights();
        selectedFigure = null;
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        // Draw Chessboard
        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        // Draw Selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * (selectionX + 1),
               Vector3.forward * (selectionY + 1) + Vector3.right * selectionX);
        }
    }

    private void UpdateSelection()
    {
        if (!Camera.main) return;

        RaycastHit hit;
        float raycastDistance = 25.0f;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastDistance, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessFigure(int index, int x, int y)
    {
        GameObject go = Instantiate(chessFigures[index], GetTileCenter(x, y), chessFigures[index].transform.rotation) as GameObject;
        go.transform.SetParent(transform);
        ChessFigurePositions[x, y] = go.GetComponent<ChessFigure>();
        ChessFigurePositions[x, y].SetPosition(x, y);
        activeFigures.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void SpawnAllChessFigures()
    {
        // White
        SpawnChessFigure(0, 4, 0); // King
        SpawnChessFigure(1, 3, 0); // Queen
        SpawnChessFigure(2, 0, 0); // Rook
        SpawnChessFigure(2, 7, 0); // Rook
        SpawnChessFigure(3, 2, 0); // Bishop
        SpawnChessFigure(3, 5, 0); // Bishop
        SpawnChessFigure(4, 1, 0); // Knight
        SpawnChessFigure(4, 6, 0); // Knight
        SpawnChessFigure(5, 0, 1);
        SpawnChessFigure(5, 1, 1);
        SpawnChessFigure(5, 2, 1);
        SpawnChessFigure(5, 3, 1);
        SpawnChessFigure(5, 4, 1);
        SpawnChessFigure(5, 5, 1);
        SpawnChessFigure(5, 6, 1);
        SpawnChessFigure(5, 7, 1);

        // Black
        SpawnChessFigure(6, 4, 7); // King
        SpawnChessFigure(7, 3, 7); // Queen
        SpawnChessFigure(8, 0, 7); // Rook
        SpawnChessFigure(8, 7, 7); // Rook
        SpawnChessFigure(9, 2, 7); // Bishop
        SpawnChessFigure(9, 5, 7); // Bishop
        SpawnChessFigure(10, 1, 7); // Knight
        SpawnChessFigure(10, 6, 7); // Knight
        SpawnChessFigure(11, 0, 6);
        SpawnChessFigure(11, 1, 6);
        SpawnChessFigure(11, 2, 6);
        SpawnChessFigure(11, 3, 6);
        SpawnChessFigure(11, 4, 6);
        SpawnChessFigure(11, 5, 6);
        SpawnChessFigure(11, 6, 6);
        SpawnChessFigure(11, 7, 6);
    }

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team won!");
        else
            Debug.Log("Black team won!");

        foreach (GameObject go in activeFigures)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlighting.Instance.HideHighlights();
        SpawnAllChessFigures();
        Debug.Log("White turn: " + isWhiteTurn);
    }
}

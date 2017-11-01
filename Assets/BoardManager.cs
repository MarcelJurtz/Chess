using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;
    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessFigures;
    private List<GameObject> activeFigures = new List<GameObject>();

    void Start()
    {
        // White
        SpawnChessFigure(0, GetTileCenter(3, 0)); // King
        SpawnChessFigure(1, GetTileCenter(4, 0)); // Queen
        SpawnChessFigure(2, GetTileCenter(0, 0)); // Rook
        SpawnChessFigure(2, GetTileCenter(7, 0)); // Rook
        SpawnChessFigure(3, GetTileCenter(2, 0)); // Bishop
        SpawnChessFigure(3, GetTileCenter(5, 0)); // Bishop
        SpawnChessFigure(4, GetTileCenter(1, 0)); // Knight
        SpawnChessFigure(4, GetTileCenter(6, 0)); // Knight
        SpawnChessFigure(5, GetTileCenter(0, 1));
        SpawnChessFigure(5, GetTileCenter(1, 1));
        SpawnChessFigure(5, GetTileCenter(2, 1));
        SpawnChessFigure(5, GetTileCenter(3, 1));
        SpawnChessFigure(5, GetTileCenter(4, 1));
        SpawnChessFigure(5, GetTileCenter(5, 1));
        SpawnChessFigure(5, GetTileCenter(6, 1));
        SpawnChessFigure(5, GetTileCenter(7, 1));

        // Black
        SpawnChessFigure(6, GetTileCenter(4, 7)); // King
        SpawnChessFigure(7, GetTileCenter(3, 7)); // Queen
        SpawnChessFigure(8, GetTileCenter(0, 7)); // Rook
        SpawnChessFigure(8, GetTileCenter(7, 7)); // Rook
        SpawnChessFigure(9, GetTileCenter(2, 7)); // Bishop
        SpawnChessFigure(9, GetTileCenter(5, 7)); // Bishop
        SpawnChessFigure(10, GetTileCenter(1, 7)); // Knight
        SpawnChessFigure(10, GetTileCenter(6, 7)); // Knight
        SpawnChessFigure(11, GetTileCenter(0, 6));
        SpawnChessFigure(11, GetTileCenter(1, 6));
        SpawnChessFigure(11, GetTileCenter(2, 6));
        SpawnChessFigure(11, GetTileCenter(3, 6));
        SpawnChessFigure(11, GetTileCenter(4, 6));
        SpawnChessFigure(11, GetTileCenter(5, 6));
        SpawnChessFigure(11, GetTileCenter(6, 6));
        SpawnChessFigure(11, GetTileCenter(7, 6));
    }

    void Update()
    {
        DrawChessBoard();
        UpdateSelection();
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

    private void SpawnChessFigure(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessFigures[index], position, chessFigures[index].transform.rotation) as GameObject;
        go.transform.SetParent(transform);
        activeFigures.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
}

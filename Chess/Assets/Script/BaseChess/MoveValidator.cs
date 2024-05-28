using UnityEngine;
using System.Collections.Generic;

public static class MoveValidator
{
    public static bool IsMoveLegal(GameObject piece, int targetX, int targetY, GameObject[,] positions, string currentPlayer, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        Chessman chessman = piece.GetComponent<Chessman>();
        int originalX = chessman.GetXBoard();
        int originalY = chessman.GetYBoard();
        GameObject targetPiece = positions[targetX, targetY];

        positions[originalX, originalY] = null;
        chessman.SetXBoard(targetX);
        chessman.SetYBoard(targetY);
        positions[targetX, targetY] = piece;

        if (targetPiece != null) targetPiece.SetActive(false);

        bool isInCheck = CheckValidator.IsInCheck(currentPlayer, positions, playerWhite, playerBlack);

        positions[targetX, targetY] = targetPiece;
        chessman.SetXBoard(originalX);
        chessman.SetYBoard(originalY);
        positions[originalX, originalY] = piece;

        if (targetPiece != null)
        {
            targetPiece.SetActive(true);
        }

        return !isInCheck;
    }
}
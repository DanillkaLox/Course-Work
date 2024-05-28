using UnityEngine;
using System.Collections.Generic;

public static class GameOverChecker
{
    public static bool IsGameOver(GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        return IsCheckmate("white", positions, playerWhite, playerBlack) || IsCheckmate("black", positions, playerWhite, playerBlack);
    }

    private static bool IsCheckmate(string currentPlayer, GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        GameObject[] currentPlayerPieces = currentPlayer == "white" ? playerWhite : playerBlack;

        foreach (var piece in currentPlayerPieces)
        {
            if (piece == null || !piece.activeSelf) continue;

            Chessman cm = piece.GetComponent<Chessman>();
            List<Vector2> possibleMoves = cm.GetPossibleMoves();

            foreach (var move in possibleMoves)
            {
                if (MoveValidator.IsMoveLegal(piece, (int)move.x, (int)move.y, positions, currentPlayer, playerWhite, playerBlack))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
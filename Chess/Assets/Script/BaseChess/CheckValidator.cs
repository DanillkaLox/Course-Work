using System.Collections.Generic;
using UnityEngine;

public static class CheckValidator
{
    public static bool IsInCheck(string currentPlayer, GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        string kingName = currentPlayer == "white" ? "white_king" : "black_king";
        GameObject king = null;

        foreach (var piece in currentPlayer == "white" ? playerWhite : playerBlack)
        {
            if (piece != null && piece.name == kingName && piece.activeSelf)
            {
                king = piece;
                break;
            }
        }

        if (king != null)
        {
            Chessman kingCm = king.GetComponent<Chessman>();
            int kingX = kingCm.GetXBoard();
            int kingY = kingCm.GetYBoard();

            foreach (var piece in currentPlayer == "white" ? playerBlack : playerWhite)
            {
                if (piece == null || !piece.activeSelf) continue;

                Chessman cm = piece.GetComponent<Chessman>();
                List<Vector2> possibleMoves = cm.GetPossibleMoves();

                foreach (var move in possibleMoves)
                {
                    if ((int)move.x == kingX && (int)move.y == kingY)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool IsInCheckmate(string currentPlayer, GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
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

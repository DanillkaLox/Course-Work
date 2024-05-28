using UnityEngine;

public static class BoardEvaluator
{
    public static int EvaluateBoard(GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        int evaluation = 0;

        foreach (var piece in playerWhite)
        {
            if (piece != null && piece.activeSelf)
            {
                evaluation += GetPieceValue(piece.name);
            }
        }

        foreach (var piece in playerBlack)
        {
            if (piece != null && piece.activeSelf)
            {
                evaluation -= GetPieceValue(piece.name);
            }
        }

        return -evaluation;
    }

    private static int GetPieceValue(string pieceName)
    {
        switch (pieceName)
        {
            case "white_pawn":
            case "black_pawn": return 100;
            case "white_knight":
            case "black_knight": return 320;
            case "white_bishop":
            case "black_bishop": return 330;
            case "white_rook":
            case "black_rook": return 500;
            case "white_queen":
            case "black_queen": return 900;
            default: return 0;
        }
    }
}
using UnityEngine;

public static class BoardEvaluator
{
    private static Game _game;

    public static void Initialize(Game game)
    {
        _game = game;
    }

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

        if (_game.IsInCheckmate())
        {
            if (_game.GetCurrentPlayer() == "white")
                evaluation += 20000;
            else
                evaluation -= 20000;
        }
        else if (_game.IsInCheck())
        {
            if (_game.GetCurrentPlayer() == "white")
                evaluation += 500;
            else
                evaluation -= 500;
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
using System.Collections.Generic;
using UnityEngine;

public static class MinimaxAlgorithm
{
    public static Move GetBestMove(GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack, string currentPlayer)
    {
        Move bestMove = null;
        int bestValue = int.MinValue;
        List<Move> moves = GetAllPossibleMoves(currentPlayer, positions, playerWhite, playerBlack);

        foreach (var move in moves)
        {
            MakeMove(move, positions);
            int boardValue = Minimax(positions, playerWhite, playerBlack, 2, int.MinValue, int.MaxValue, false);
            UndoMove(move, positions);

            if (boardValue > bestValue)
            {
                bestValue = boardValue;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private static int Minimax(GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack, int depth, int alpha, int beta, bool isMaximizingPlayer)
    {
        if (depth == 0 || GameOverChecker.IsGameOver(positions, playerWhite, playerBlack))
        {
            return BoardEvaluator.EvaluateBoard(positions, playerWhite, playerBlack);
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in GetAllPossibleMoves("black", positions, playerWhite, playerBlack))
            {
                MakeMove(move, positions);
                int eval = Minimax(positions, playerWhite, playerBlack, depth - 1, alpha, beta, false);
                UndoMove(move, positions);
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in GetAllPossibleMoves("white", positions, playerWhite, playerBlack))
            {
                MakeMove(move, positions);
                int eval = Minimax(positions, playerWhite, playerBlack, depth - 1, alpha, beta, true);
                UndoMove(move, positions);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }

    private static void MakeMove(Move move, GameObject[,] positions)
    {
        move.CapturedPiece = positions[move.TargetX, move.TargetY];
        if (move.CapturedPiece != null) move.CapturedPiece.SetActive(false);

        Chessman cm = move.Piece.GetComponent<Chessman>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = null;
        cm.SetXBoard(move.TargetX);
        cm.SetYBoard(move.TargetY);
        positions[move.TargetX, move.TargetY] = move.Piece;
    }

    private static void UndoMove(Move move, GameObject[,] positions)
    {
        Chessman cm = move.Piece.GetComponent<Chessman>();
        positions[move.TargetX, move.TargetY] = move.CapturedPiece;
        if (move.CapturedPiece != null) move.CapturedPiece.SetActive(true);

        cm.SetXBoard(move.StartX);
        cm.SetYBoard(move.StartY);
        positions[move.StartX, move.StartY] = move.Piece;
    }

    private static List<Move> GetAllPossibleMoves(string player, GameObject[,] positions, GameObject[] playerWhite, GameObject[] playerBlack)
    {
        List<Move> moves = new List<Move>();
        GameObject[] pieces = player == "white" ? playerWhite : playerBlack;

        foreach (var piece in pieces)
        {
            if (piece != null && piece.activeSelf)
            {
                Chessman cm = piece.GetComponent<Chessman>();
                List<Vector2> possibleMoves = cm.GetPossibleMoves();

                foreach (var move in possibleMoves)
                {
                    if (MoveValidator.IsMoveLegal(piece, (int)move.x, (int)move.y, positions, player, playerWhite, playerBlack))
                    {
                        moves.Add(new Move(piece, (int)move.x, (int)move.y));
                    }
                }
            }
        }

        return moves;
    }
}

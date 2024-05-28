using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;

    public GameObject winnerText;

    public GameObject gameOverMenu;

    private readonly GameObject[,] _positions = new GameObject[8, 8];
    public GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];
    public GameObject[] playerWhiteActive = new GameObject[16];
    public GameObject[] playerBlackActive = new GameObject[16];

    private string _currentPlayer = "white";

    private bool _gameOver;
    
    private Stack<Move> _moveHistory = new Stack<Move>();

    public void Start()
    {
        Time.timeScale = 1f;

        playerWhite = new[]
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        playerBlack = new[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7),
            Create("black_bishop", 2, 7), Create("black_queen", 3, 7), Create("black_king", 4, 7),
            Create("black_bishop", 5, 7), Create("black_knight", 6, 7), Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        foreach (var piece in playerBlack)
        {
            SetPosition(piece);
        }

        foreach (var piece in playerWhite)
        {
            SetPosition(piece);
        }
    }

    public GameObject Create(string pieceName, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = pieceName;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        SetPosition(obj);
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        _positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        _positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return _positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _positions.GetLength(0) && y < _positions.GetLength(1);
    }

    public string GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public void NextTurn()
    {
        _currentPlayer = _currentPlayer == "white" ? "black" : "white";

        if (IsInCheckmate())
        {
            Winner(_currentPlayer == "white" ? "black" : "white");
        }
        else if (IsInCheck())
        {
            Debug.Log(_currentPlayer + " is in check!");
        }

        if (_currentPlayer == "black")
        {
            MakeBestMove();
        }
    }

    public void Update()
    {
        if (_gameOver)
        {
            GameOver();
            _gameOver = false;
        }
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Winner(string playerWinner)
    {
        _gameOver = true;

        winnerText.GetComponent<Text>().text = playerWinner + " IS THE WINNER";
    }

    private bool IsInCheck()
    {
        string kingName = _currentPlayer == "white" ? "white_king" : "black_king";
        GameObject king = null;

        foreach (var piece in _currentPlayer == "white" ? playerWhite : playerBlack)
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

            foreach (var piece in _currentPlayer == "white" ? playerBlack : playerWhite)
            {
                if (piece == null || !piece.activeSelf)
                    continue;

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

    private bool IsInCheckmate()
    {
        foreach (var piece in _currentPlayer == "white" ? playerWhite : playerBlack)
        {
            if (piece == null || !piece.activeSelf) continue;

            Chessman cm = piece.GetComponent<Chessman>();
            List<Vector2> possibleMoves = cm.GetPossibleMoves();

            foreach (var move in possibleMoves)
            {
                int oldX = cm.GetXBoard();
                int oldY = cm.GetYBoard();
                GameObject oldPiece = GetPosition((int)move.x, (int)move.y);

                SetPositionEmpty(oldX, oldY);
                cm.SetXBoard((int)move.x);
                cm.SetYBoard((int)move.y);
                SetPosition(piece);

                if (oldPiece != null) oldPiece.SetActive(false);

                bool wasCheck = IsInCheck();

                SetPositionEmpty((int)move.x, (int)move.y);
                cm.SetXBoard(oldX);
                cm.SetYBoard(oldY);
                SetPosition(piece);

                if (oldPiece != null)
                {
                    oldPiece.SetActive(true);
                    SetPosition(oldPiece);
                }

                if (!wasCheck) return false;
            }
        }

        return true;
    }

    public void RemovePiece(GameObject piece)
    {
        if (piece == null) return;

        Chessman cm = piece.GetComponent<Chessman>();
        List<GameObject> opponentPieces = new List<GameObject>(_currentPlayer == "white" ? playerBlack : playerWhite);

        for (int i = 0; i < opponentPieces.Count; i++)
        {
            if (opponentPieces[i] == piece)
            {
                opponentPieces.RemoveAt(i);
                break;
            }
        }

        piece.SetActive(false);

        SetPositionEmpty(cm.GetXBoard(), cm.GetYBoard());
    }

    public bool IsMoveLegal(GameObject piece, int targetX, int targetY)
    {
        Chessman chessman = piece.GetComponent<Chessman>();
        int originalX = chessman.GetXBoard();
        int originalY = chessman.GetYBoard();
        GameObject targetPiece = GetPosition(targetX, targetY);

        SetPositionEmpty(originalX, originalY);
        chessman.SetXBoard(targetX);
        chessman.SetYBoard(targetY);
        SetPosition(piece);

        if (targetPiece != null) targetPiece.SetActive(false);

        bool isInCheck = IsInCheck();

        SetPositionEmpty(targetX, targetY);
        chessman.SetXBoard(originalX);
        chessman.SetYBoard(originalY);
        SetPosition(piece);

        if (targetPiece != null)
        {
            targetPiece.SetActive(true);
            SetPosition(targetPiece);
        }

        return !isInCheck;
    }

    public int EvaluateBoard()
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

        if (IsInCheckmate())
        {
            if (_currentPlayer == "white")
                evaluation += 20000;
            else
                evaluation -= 20000;
        }
        else if (IsInCheck())
        {
            if (_currentPlayer == "white")
                evaluation += 500;
            else
                evaluation -= 500;
        }

        return -evaluation;
    }

    private int GetPieceValue(string pieceName)
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

    public List<Move> GetAllPossibleMoves(string player)
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
                    if (IsMoveLegal(piece, (int)move.x, (int)move.y))
                    {
                        moves.Add(new Move(piece, (int)move.x, (int)move.y));
                    }
                }
            }
        }

        return moves;
    }

    public int Minimax(int depth, int alpha, int beta, bool isMaximizingPlayer)
    {
        if (depth == 0 || IsGameOver())
        {
            return EvaluateBoard();
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in GetAllPossibleMoves("black"))
            {
                if (IsMoveLegal(move.Piece, move.TargetX, move.TargetY))
                {
                    MakeMove(move);
                    int eval = Minimax(depth - 1, alpha, beta, false);
                    UndoMove(move);
                    maxEval = Mathf.Max(maxEval, eval);
                    alpha = Mathf.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }

            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in GetAllPossibleMoves("white"))
            {
                if (IsMoveLegal(move.Piece, move.TargetX, move.TargetY))
                {
                    MakeMove(move);
                    int eval = Minimax(depth - 1, alpha, beta, true);
                    UndoMove(move);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }

            return minEval;
        }
    }

    private void MakeMove(Move move)
    {
        move.CapturedPiece = GetPosition(move.TargetX, move.TargetY);

        if (move.CapturedPiece != null)
        {
            move.CapturedPiece.SetActive(false);
        }

        SetPositionEmpty(move.StartX, move.StartY);
        move.Piece.GetComponent<Chessman>().SetXBoard(move.TargetX);
        move.Piece.GetComponent<Chessman>().SetYBoard(move.TargetY);
        SetPosition(move.Piece);

        _moveHistory.Push(move);
    }

    private void UndoMove(Move move)
    {
        SetPositionEmpty(move.Piece.GetComponent<Chessman>().GetXBoard(), move.Piece.GetComponent<Chessman>().GetYBoard());
        move.Piece.GetComponent<Chessman>().SetXBoard(move.StartX);
        move.Piece.GetComponent<Chessman>().SetYBoard(move.StartY);
        SetPosition(move.Piece);

        if (move.CapturedPiece != null)
        {
            move.CapturedPiece.SetActive(true);
            SetPosition(move.CapturedPiece);
        }

        _moveHistory.Pop();
    }

    public void MakeBestMove()
    {
        Move bestMove = null;
        int bestValue = int.MinValue;
        List<Move> moves = GetAllPossibleMoves("black");
        
        foreach (var move in moves)
        {
            MakeMove(move);
            int boardValue = Minimax(2, int.MinValue, int.MaxValue, false);
            UndoMove(move);

            if (boardValue > bestValue)
            {
                bestValue = boardValue;
                bestMove = move;
            }
        }

        if (bestMove != null)
        {
            MakeMove(bestMove);
            bestMove.Piece.GetComponent<Chessman>().MovePiece(bestMove.TargetX, bestMove.TargetY);
            NextTurn();
        }
    }
}
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
    private string _currentPlayer = "white";
    private bool _gameOver;
    private Stack<Move> _moveHistory = new Stack<Move>();

    public void Start()
    {
        Time.timeScale = 1f;
        InitializePieces();
        PlacePiecesOnBoard();
    }

    private void InitializePieces()
    {
        playerWhite = PieceInitializer.InitializeWhitePieces(chessPiece);
        playerBlack = PieceInitializer.InitializeBlackPieces(chessPiece);
    }

    private void PlacePiecesOnBoard()
    {
        foreach (var piece in playerBlack)
        {
            SetPosition(piece);
        }

        foreach (var piece in playerWhite)
        {
            SetPosition(piece);
        }
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
        return CheckValidator.IsInCheck(_currentPlayer, _positions, playerWhite, playerBlack);
    }

    private bool IsInCheckmate()
    {
        return CheckValidator.IsInCheckmate(_currentPlayer, _positions, playerWhite, playerBlack);
    }

    public void RemovePiece(GameObject piece)
    {
        if (piece == null) return;

        Chessman cm = piece.GetComponent<Chessman>();
        piece.SetActive(false);
        SetPositionEmpty(cm.GetXBoard(), cm.GetYBoard());
    }

    public bool IsMoveLegal(GameObject piece, int targetX, int targetY)
    {
        return MoveValidator.IsMoveLegal(piece, targetX, targetY, _positions, _currentPlayer, playerWhite, playerBlack);
    }

    public void MakeBestMove()
    {
        Move bestMove = MinimaxAlgorithm.GetBestMove(_positions, playerWhite, playerBlack, _currentPlayer);
        if (bestMove != null)
        {
            ExecuteMove(bestMove);
        }
    }

    private void ExecuteMove(Move move)
    {
        move.Piece.GetComponent<Chessman>().MovePiece(move.TargetX, move.TargetY);
        NextTurn();
    }
}

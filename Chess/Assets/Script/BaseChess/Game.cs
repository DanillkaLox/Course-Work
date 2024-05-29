using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    public GameObject winnerText;
    public GameObject gameOverMenu;
    public GameObject board;

    private readonly GameObject[,] _positions = new GameObject[8, 8];
    public GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];
    private string _currentPlayer = "white";
    private bool _gameOver;

    private bool _setupMode = true;

    public GameObject startGameButton;
    public GameObject trashButton;
    public GameObject piecePanal;

    private int whiteKingCount;
    private int blackKingCount;
    private int whitePawnCount;
    private int blackPawnCount;
    private int whitePieceCount;
    private int blackPieceCount;
    private int whiteKnightCount;
    private int blackKnightCount;
    private int whiteBishopCount;
    private int blackBishopCount;
    private int whiteRookCount;
    private int blackRookCount;
    private int whiteQueenCount;
    private int blackQueenCount;

    public void Start()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "Custom")
        {
            InitializePieces();
            PlacePiecesOnBoard();
            BoardEvaluator.Initialize(this);
            _setupMode = false;
        }
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

        if (_currentPlayer == "black" && SceneManager.GetActiveScene().name != "1VS1")
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

    public bool IsInCheck()
    {
        return CheckValidator.IsInCheck(_currentPlayer, _positions, playerWhite, playerBlack);
    }

    public bool IsInCheckmate()
    {
        return CheckValidator.IsInCheckmate(_currentPlayer, _positions, playerWhite, playerBlack);
    }

    public void RemovePiece(GameObject piece)
    {
        if (piece == null) return;
        int temp = 0;

        Chessman cm = piece.GetComponent<Chessman>();
        piece.SetActive(false);
        SetPositionEmpty(cm.GetXBoard(), cm.GetYBoard());
        if (_setupMode)
        {
            if (cm.player == "white")
            {
                whitePieceCount--;
                if (cm.name.Contains("pawn")) whitePawnCount--;
                if (cm.name.Contains("king")) whiteKingCount--;
                if (cm.name.Contains("knight"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.name.Contains("knight"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        whiteKnightCount--;
                    }
                    else
                    {
                        whitePawnCount--;
                    }
                }

                if (cm.name.Contains("bishop"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.name.Contains("bishop"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        whiteBishopCount--;
                    }
                    else
                    {
                        whitePawnCount--;
                    }
                }

                if (cm.name.Contains("rook"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.name.Contains("rook"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        whiteRookCount--;
                    }
                    else
                    {
                        whitePawnCount--;
                    }
                }

                if (cm.name.Contains("queen"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.name.Contains("queen"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 1)
                    {
                        whiteQueenCount--;
                    }
                    else
                    {
                        whitePawnCount--;
                    }
                }
            }
            else
            {
                blackPieceCount--;
                if (cm.name.Contains("king")) blackKingCount--;
                if (cm.name.Contains("pawn")) blackPawnCount--;
                if (cm.name.Contains("knight"))
                {
                    if (cm.name.Contains("knight"))
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            if (playerBlack[i] == cm.name.Contains("knight"))
                            {
                                temp++;
                            }
                        }
                        if (temp <= 2)
                        {
                            blackKnightCount--;
                        }
                        else
                        {
                            blackPawnCount--;
                        }
                    }
                }
                if (cm.name.Contains("bishop"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.name.Contains("bishop"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        blackBishopCount--;
                    }
                    else
                    {
                        blackPawnCount--;
                    }
                }

                if (cm.name.Contains("rook"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.name.Contains("rook"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        blackRookCount--;
                    }
                    else
                    {
                        blackPawnCount--;
                    }
                }

                if (cm.name.Contains("queen"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.name.Contains("queen"))
                        {
                            temp++;
                        }
                    }
                    if (temp <= 1)
                    {
                        blackQueenCount--;
                    }
                    else
                    {
                        blackPawnCount--;
                    }
                }
            }
        }
    }

    public bool IsMoveLegal(GameObject piece, int targetX, int targetY)
    {
        return MoveValidator.IsMoveLegal(piece, targetX, targetY, _positions, _currentPlayer, playerWhite, playerBlack);
    }

    private void MakeBestMove()
    {
        Move bestMove = MinimaxAlgorithm.GetBestMove(_positions, playerWhite, playerBlack, _currentPlayer);
        if (bestMove != null)
        {
            if (bestMove.CapturedPiece != null)
            {
                RemovePiece(bestMove.CapturedPiece);
            }

            ExecuteMove(bestMove);

        }
    }

    private void ExecuteMove(Move move)
    {
        move.Piece.GetComponent<Chessman>().MovePiece(move.TargetX, move.TargetY);
        NextTurn();
    }

    public void StartGame()
    {
        _setupMode = false;
        startGameButton.SetActive(false);
        trashButton.SetActive(false);
        piecePanal.SetActive(false);
        BoardEvaluator.Initialize(this);
    }

    public bool IsSetupMode()
    {
        return _setupMode;
    }

    public bool CanAddPiece(string pieceName, string player)
    {
        if (player == "white")
        {
            if (whitePieceCount >= 16) return false;
            if (pieceName.Contains("king") && whiteKingCount >= 1) return false;
            if (pieceName.Contains("pawn") && whitePawnCount >= 8) return false;

            int maxAdditionalPieces = 8 - whitePawnCount;

            if (pieceName.Contains("knight") && whiteKnightCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("bishop") && whiteBishopCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("rook") && whiteRookCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("queen") && whiteQueenCount >= 1 + maxAdditionalPieces) return false;
        }
        else
        {
            if (blackPieceCount >= 16) return false;
            if (pieceName.Contains("king") && blackKingCount >= 1) return false;
            if (pieceName.Contains("pawn") && blackPawnCount >= 8) return false;

            int maxAdditionalPieces = 8 - blackPawnCount;

            if (pieceName.Contains("knight") && blackKnightCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("bishop") && blackBishopCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("rook") && blackRookCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("queen") && blackQueenCount >= 1 + maxAdditionalPieces) return false;
        }

        return true;
    }

    public void AddPiece(string pieceName, GameObject piece)
    {
        if (piece.GetComponent<Chessman>().player == "white")
        {
            whitePieceCount++;
            if (pieceName.Contains("king")) whiteKingCount++;
            if (pieceName.Contains("pawn")) whitePawnCount++;
            if (pieceName.Contains("knight"))
            {
                if (whiteKnightCount <= 1)
                {
                    whiteKnightCount++;
                }
                else
                {
                    whitePawnCount++;
                }
            }

            if (pieceName.Contains("bishop"))
            {
                if (whiteBishopCount <= 1)
                {
                    whiteBishopCount++;
                }
                else
                {
                    whitePawnCount++;
                }
                
            }

            if (pieceName.Contains("rook"))
            {
                if (whiteRookCount <= 1)
                {
                    whiteRookCount++;
                }
                else
                {
                    whitePawnCount++;
                }
            }

            if (pieceName.Contains("queen"))
            {
                if (whiteQueenCount == 0)
                {
                    whiteQueenCount++;
                }
                else
                {
                    whitePawnCount++;
                }
            }
        }
        else
        {
            blackPieceCount++;
            if (pieceName.Contains("king")) blackKingCount++;
            if (pieceName.Contains("pawn")) blackPawnCount++;
            if (pieceName.Contains("knight"))
            {
                if (blackKnightCount <= 1)
                {
                    blackKnightCount++;
                }
                else
                {
                    blackPawnCount++;
                }
            }

            if (pieceName.Contains("bishop"))
            {
                if (blackBishopCount <= 1)
                {
                    blackBishopCount++;
                }
                else
                {
                    blackPawnCount++;
                }
            }

            if (pieceName.Contains("rook"))
            {
                if (blackRookCount <= 1)
                {
                    blackRookCount++;
                }
                else
                {
                    blackPawnCount++;
                }
            }

            if (pieceName.Contains("queen"))
            {
                if (blackQueenCount == 0)
                {
                    blackQueenCount++;
                }
                else
                {
                    blackPawnCount++;
                }
            }
        }
    }
}


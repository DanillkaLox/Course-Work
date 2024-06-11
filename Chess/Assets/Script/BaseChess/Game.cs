using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    public GameObject winnerText;
    public GameObject gameOverMenu;
    public GameObject board;
    public GameObject turnText;

    private GameObject[,] _positions = new GameObject[8, 8];
    public GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];
    private string _currentPlayer = "white";
    private bool _gameOver;

    private bool _setupMode = true;

    public GameObject startGameButton;
    public GameObject trashButton;
    public GameObject piecePanal;
    public GameObject pcButton;
    public GameObject vsButton;
    public GameObject whiteButton;
    public GameObject blackButton;

    public int customMode = 1;

    private int _whiteKingCount;
    private int _blackKingCount;
    private int _whitePawnCount;
    private int _blackPawnCount;
    private int _whitePieceCount;
    private int _blackPieceCount;
    private int _whiteKnightCount;
    private int _blackKnightCount;
    private int _whiteBishopCount;
    private int _blackBishopCount;
    private int _whiteRookCount;
    private int _blackRookCount;
    private int _whiteQueenCount;
    private int _blackQueenCount;

    public void Start()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "Custom")
        {
            InitializePieces();
            PlacePiecesOnBoard();
            BoardEvaluator.Initialize(this);
            _setupMode = false;
            turnText.SetActive(true);
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
    
    public void WhitePlayer()
    {
        _currentPlayer = "white";
    }
    public void BlackPlayer()
    {
        _currentPlayer = "black";
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public void NextTurn()
    {
        _currentPlayer = _currentPlayer == "white" ? "black" : "white";
        turnText.GetComponent<Text>().text = _currentPlayer == "white" ? "WHITE TURN" : "BLACK TURN";
        if (IsInCheckmate() && IsInCheck())
        {
            Winner(_currentPlayer == "white" ? "black" : "white");
        }
        else if (IsInCheckmate() && !IsInCheck())
        {
            Stalemate();
        }
        else if (IsInCheck())
        {
            Debug.Log(_currentPlayer + " is in check!");
        }

        if (_currentPlayer == "black" && (SceneManager.GetActiveScene().name == "VSComputer" || customMode == 2))
        {
            Invoke(nameof(MakeBestMove), 0.1f);
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
        var player = playerWinner == "white" ? "WHITE" : "BLACK";
        winnerText.GetComponent<Text>().text = player + " IS THE WINNER";
    }

    private void Stalemate()
    {
        _gameOver = true;
        winnerText.GetComponent<Text>().text = "STALEMATE";
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
                _whitePieceCount--;
                if (cm.name.Contains("pawn")) _whitePawnCount--;
                if (cm.name.Contains("king")) _whiteKingCount--;
                if (cm.name.Contains("knight"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        _whiteKnightCount--;
                    }
                    else
                    {
                        _whitePawnCount--;
                    }
                }

                if (cm.name.Contains("bishop"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        _whiteBishopCount--;
                    }
                    else
                    {
                        _whitePawnCount--;
                    }
                }

                if (cm.name.Contains("rook"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        _whiteRookCount--;
                    }
                    else
                    {
                        _whitePawnCount--;
                    }
                }

                if (cm.name.Contains("queen"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerWhite[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 1)
                    {
                        _whiteQueenCount--;
                    }
                    else
                    {
                        _whitePawnCount--;
                    }
                }
            }
            else
            {
                _blackPieceCount--;
                if (cm.name.Contains("king")) _blackKingCount--;
                if (cm.name.Contains("pawn")) _blackPawnCount--;
                if (cm.name.Contains("knight"))
                {
                    if (cm.name.Contains("knight"))
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            if (playerBlack[i] == cm.gameObject)
                            {
                                temp++;
                            }
                        }
                        if (temp <= 2)
                        {
                            _blackKnightCount--;
                        }
                        else
                        {
                            _blackPawnCount--;
                        }
                    }
                }
                if (cm.name.Contains("bishop"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        _blackBishopCount--;
                    }
                    else
                    {
                        _blackPawnCount--;
                    }
                }

                if (cm.name.Contains("rook"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 2)
                    {
                        _blackRookCount--;
                    }
                    else
                    {
                        _blackPawnCount--;
                    }
                }

                if (cm.name.Contains("queen"))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (playerBlack[i] == cm.gameObject)
                        {
                            temp++;
                        }
                    }
                    if (temp <= 1)
                    {
                        _blackQueenCount--;
                    }
                    else
                    {
                        _blackPawnCount--;
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
        turnText.SetActive(true);
        AudioManager.Instance.Play("ButtonSound");
        if (_whiteKingCount != 0 && _blackKingCount != 0)
        {
            _currentPlayer = _currentPlayer == "white" ? "black" : "white";
            if (IsInCheckmate() && IsInCheck())
            {
                
                turnText.GetComponent<Text>().text = "It`s checkmate";
                turnText.GetComponent<Text>().fontSize = 30;
            }
            else if (IsInCheckmate() && !IsInCheck())
            {
                turnText.GetComponent<Text>().text = "It`s Stalemate";
                turnText.GetComponent<Text>().fontSize = 30;
            }
            else
            {
                _currentPlayer = _currentPlayer == "white" ? "black" : "white";
                if (IsInCheckmate() && IsInCheck())
                {
                    turnText.GetComponent<Text>().text = "It`s checkmate";
                    turnText.GetComponent<Text>().fontSize = 30;
                }
                else if (IsInCheckmate() && !IsInCheck())
                {
                    turnText.GetComponent<Text>().text = "It`s Stalemate";
                    turnText.GetComponent<Text>().fontSize = 30;
                }
                else
                {
                    _currentPlayer = _currentPlayer == "white" ? "black" : "white";
                    if (IsInCheck())
                    {
                        turnText.GetComponent<Text>().text = "Opponent in check";
                        turnText.GetComponent<Text>().fontSize = 30;
                        _currentPlayer = _currentPlayer == "white" ? "black" : "white";
                    }
                    else
                    {
                        _currentPlayer = _currentPlayer == "white" ? "black" : "white";
                        _setupMode = false;
                        startGameButton.SetActive(false);
                        trashButton.SetActive(false);
                        piecePanal.SetActive(false);
                        vsButton.SetActive(false);
                        pcButton.SetActive(false);
                        whiteButton.SetActive(false);
                        blackButton.SetActive(false);
                        turnText.GetComponent<Text>().fontSize = 35;
                        turnText.GetComponent<Text>().text = _currentPlayer == "white" ? "WHITE TURN" : "BLACK TURN";
                        BoardEvaluator.Initialize(this);
                        if (_currentPlayer == "black" && customMode == 2)
                        {
                            Invoke(nameof(MakeBestMove), 0.1f);
                        }
                    }
                }
            }
        }
        else
        {
            turnText.GetComponent<Text>().text = "On board must be 2 kings";
            turnText.GetComponent<Text>().fontSize = 30;
        }
    }

    public bool IsSetupMode()
    {
        return _setupMode;
    }

    public bool CanAddPiece(string pieceName, string player)
    {
        if (player == "white")
        {
            if (_whitePieceCount >= 16) return false;
            if (pieceName.Contains("king") && _whiteKingCount >= 1) return false;
            if (pieceName.Contains("pawn") && _whitePawnCount >= 8) return false;

            int maxAdditionalPieces = 8 - _whitePawnCount;

            if (pieceName.Contains("knight") && _whiteKnightCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("bishop") && _whiteBishopCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("rook") && _whiteRookCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("queen") && _whiteQueenCount >= 1 + maxAdditionalPieces) return false;
        }
        else
        {
            if (_blackPieceCount >= 16) return false;
            if (pieceName.Contains("king") && _blackKingCount >= 1) return false;
            if (pieceName.Contains("pawn") && _blackPawnCount >= 8) return false;

            int maxAdditionalPieces = 8 - _blackPawnCount;

            if (pieceName.Contains("knight") && _blackKnightCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("bishop") && _blackBishopCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("rook") && _blackRookCount >= 2 + maxAdditionalPieces) return false;
            if (pieceName.Contains("queen") && _blackQueenCount >= 1 + maxAdditionalPieces) return false;
        }

        return true;
    }

    public void AddPiece(string pieceName, GameObject piece)
    {
        if (piece.GetComponent<Chessman>().player == "white")
        {
            _whitePieceCount++;
            if (pieceName.Contains("king")) _whiteKingCount++;
            if (pieceName.Contains("pawn")) _whitePawnCount++;
            if (pieceName.Contains("knight"))
            {
                if (_whiteKnightCount <= 1)
                {
                    _whiteKnightCount++;
                }
                else
                {
                    _whitePawnCount++;
                }
            }

            if (pieceName.Contains("bishop"))
            {
                if (_whiteBishopCount <= 1)
                {
                    _whiteBishopCount++;
                }
                else
                {
                    _whitePawnCount++;
                }
                
            }

            if (pieceName.Contains("rook"))
            {
                if (_whiteRookCount <= 1)
                {
                    _whiteRookCount++;
                }
                else
                {
                    _whitePawnCount++;
                }
            }

            if (pieceName.Contains("queen"))
            {
                if (_whiteQueenCount == 0)
                {
                    _whiteQueenCount++;
                }
                else
                {
                    _whitePawnCount++;
                }
            }
        }
        else
        {
            _blackPieceCount++;
            if (pieceName.Contains("king")) _blackKingCount++;
            if (pieceName.Contains("pawn")) _blackPawnCount++;
            if (pieceName.Contains("knight"))
            {
                if (_blackKnightCount <= 1)
                {
                    _blackKnightCount++;
                }
                else
                {
                    _blackPawnCount++;
                }
            }

            if (pieceName.Contains("bishop"))
            {
                if (_blackBishopCount <= 1)
                {
                    _blackBishopCount++;
                }
                else
                {
                    _blackPawnCount++;
                }
            }

            if (pieceName.Contains("rook"))
            {
                if (_blackRookCount <= 1)
                {
                    _blackRookCount++;
                }
                else
                {
                    _blackPawnCount++;
                }
            }

            if (pieceName.Contains("queen"))
            {
                if (_blackQueenCount == 0)
                {
                    _blackQueenCount++;
                }
                else
                {
                    _blackPawnCount++;
                }
            }
        }
    }

    public void VsMode()
    {
        AudioManager.Instance.Play("ButtonSound");
        customMode = 1;
    }
    
    public void PcMode()
    {
        AudioManager.Instance.Play("ButtonSound");
        customMode = 2;
    }

    public void SaveGame()
    {
        Save.SavePlay(this);
    }

    public void Load()
    {
        GameData data = Save.Load();
        if (data == null) return;
        
        _currentPlayer = data.currentPlayer;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (_positions[i, j] != null)
                {
                    Destroy(_positions[i, j]);
                    SetPositionEmpty(i, j);
                }
            }
        }

        for (int i = 0; i < 16; i++)
        {
            playerBlack[i] = null;
            playerWhite[i] = null;
        }

        int countWhite = 0;
        int countBlack = 0;
        foreach (var pieceData in data.pieces)
        {
            GameObject piece = PieceInitializer.CreatePiece(pieceData.pieceName, pieceData.x, pieceData.y, chessPiece);
            SetPosition(piece);
            if (pieceData.player == "white")
            {
                if (playerWhite != null) playerWhite[countWhite] = piece;
                countWhite++;
            }
            if (pieceData.player == "black")
            {
                if (playerBlack != null) playerBlack[countBlack] = piece;
                countBlack++;
            }
        }
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;
    
    private int _xBoard = -1;
    private int _yBoard = -1;

    public string player;

    [FormerlySerializedAs("white_bishop")] public Sprite whiteBishop;
    [FormerlySerializedAs("white_king")] public Sprite whiteKing;
    [FormerlySerializedAs("white_knight")] public Sprite whiteKnight;
    [FormerlySerializedAs("white_pawn")] public Sprite whitePawn;
    [FormerlySerializedAs("white_queen")] public Sprite whiteQueen;
    [FormerlySerializedAs("white_rook")] public Sprite whiteRook;
    [FormerlySerializedAs("black_bishop")] public Sprite blackBishop;
    [FormerlySerializedAs("black_king")] public Sprite blackKing;
    [FormerlySerializedAs("black_knight")] public Sprite blackKnight;
    [FormerlySerializedAs("black_pawn")] public Sprite blackPawn;
    [FormerlySerializedAs("black_queen")] public Sprite blackQueen;
    [FormerlySerializedAs("black_rook")] public Sprite blackRook;

    private bool _hasMoved;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = whiteRook; player = "white"; break;
            
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = blackKnight; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; break;
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = blackRook; player = "black"; break;
        }
    }

    private void SetCoords()
    {
        float x = _xBoard;
        float y = _yBoard;

        x *= 1.04f;
        y *= 1.04f;

        x += -3.641f;
        y += -3.639f;

        this.transform.position = new Vector3(x, y, -2.0f);    
    }

    public int GetXBoard()
    {
        return _xBoard;
    }
    
    public int GetYBoard()
    {
        return _yBoard;
    }

    public void SetXBoard(int x)
    {
        _xBoard = x;
    }
    
    public void SetYBoard(int x)
    {
        _yBoard = x;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player && Time.timeScale >= 1 && !controller.GetComponent<Game>().IsSetupMode())
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
        if (SceneManager.GetActiveScene().name == "Custom" && controller.GetComponent<PieceSelection>().delete)
        {
            controller.GetComponent<Game>().RemovePiece(this.gameObject);
            Destroy(this.gameObject);
            controller.GetComponent<PieceSelection>().DeletePieceReset();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        foreach (var plate in movePlates)
        {
            Destroy(plate);
        }
    }

    private void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(_xBoard, _yBoard - 1, _yBoard - 2);
                break;
            case "white_pawn":
                PawnMovePlate(_xBoard, _yBoard + 1, _yBoard + 2);
                break;
        }
    }


    private void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = _xBoard + xIncrement;
        int y = _yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            if (sc.IsMoveLegal(gameObject, x, y))
            {
                MovePlateSpawn(x, y);
            }
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            if (sc.IsMoveLegal(gameObject, x, y))
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    private void LMovePlate()
    {
        PointMovePlate(_xBoard + 1, _yBoard + 2);
        PointMovePlate(_xBoard - 1, _yBoard + 2);
        PointMovePlate(_xBoard + 2, _yBoard + 1);
        PointMovePlate(_xBoard + 2, _yBoard - 1);
        PointMovePlate(_xBoard + 1, _yBoard - 2);
        PointMovePlate(_xBoard - 1, _yBoard - 2);
        PointMovePlate(_xBoard - 2, _yBoard + 1);
        PointMovePlate(_xBoard - 2, _yBoard - 1);
    }

    private void SurroundMovePlate()
    {
        PointMovePlate(_xBoard, _yBoard + 1);
        PointMovePlate(_xBoard, _yBoard - 1);
        PointMovePlate(_xBoard - 1, _yBoard + 0);
        PointMovePlate(_xBoard - 1, _yBoard - 1);
        PointMovePlate(_xBoard - 1, _yBoard + 1);
        PointMovePlate(_xBoard + 1, _yBoard + 0);
        PointMovePlate(_xBoard + 1, _yBoard - 1);
        PointMovePlate(_xBoard + 1, _yBoard + 1);
        
        if (!_hasMoved)
        {
            if (player == "white" && GetXBoard() == 4 && GetYBoard() == 0)
            {
                CastlingMovePlate(0, 0, 2, 0, 3, 0, 1, false);
                CastlingMovePlate(7, 0, 6, 0, 5, 0, 0, true);
            }
            else if (player == "black" && GetXBoard() == 4 && GetYBoard() == 0)
            {
                CastlingMovePlate(0, 7, 2, 7, 3, 7, 1, false);
                CastlingMovePlate(7, 7, 6, 7, 5, 7, 0, true);
            }
        }
    }
    
    private void CastlingMovePlate(int rookX, int rookY, int kingTargetX, int kingTargetY, int emptyX1, int emptyY, int emptyX2, bool isShortCastle)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.GetPosition(rookX, rookY) != null && sc.GetPosition(rookX, rookY).GetComponent<Chessman>().name == (player == "white" ? "white_rook" : "black_rook"))
        {
            Chessman rook = sc.GetPosition(rookX, rookY).GetComponent<Chessman>();
            if (!rook._hasMoved && sc.GetPosition(emptyX1, emptyY) == null && sc.GetPosition(kingTargetX, kingTargetY) == null)
            {
                if (isShortCastle || sc.GetPosition(emptyX2, emptyY) == null)
                {
                    MovePlateSpawn(kingTargetX, kingTargetY);
                }
            }
        }
    }

    private void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                if (sc.IsMoveLegal(gameObject, x, y))
                {
                    MovePlateSpawn(x, y);
                }
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                if (sc.IsMoveLegal(gameObject, x, y))
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
    
    private void PawnMovePlate(int x, int y, int yTwoSteps)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                if (sc.IsMoveLegal(gameObject, x, y))
                {
                    MovePlateSpawn(x, y);
                }

                if (!_hasMoved && sc.PositionOnBoard(x, yTwoSteps) && sc.GetPosition(x, yTwoSteps) == null)
                {
                    if (sc.IsMoveLegal(gameObject, x, yTwoSteps))
                    {
                        MovePlateSpawn(x, yTwoSteps);
                    }
                }
            }
        }

        if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
        {
            if (sc.IsMoveLegal(gameObject, x + 1, y))
            {
                MovePlateAttackSpawn(x + 1, y);
            }
        }

        if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
        {
            if (sc.IsMoveLegal(gameObject, x - 1, y))
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePiece(int x, int y)
    {
        if ((this.name == "white_king" || this.name == "black_king") && Mathf.Abs(x - _xBoard) == 2)
        {
            int rookX = x > _xBoard ? 7 : 0;
            int rookNewX = x > _xBoard ? 5 : 3;
            Chessman rook = controller.GetComponent<Game>().GetPosition(rookX, _yBoard).GetComponent<Chessman>();
            rook.Castling(rookNewX, _yBoard);
        }
        controller.GetComponent<Game>().SetPositionEmpty(_xBoard, _yBoard);
        
        SetXBoard(x);
        SetYBoard(y);
        SetCoords();
        _hasMoved = true;
        
        controller.GetComponent<Game>().SetPosition(this.gameObject);
        
        if (name.Contains("pawn"))
        {
            if ((name.Contains("white") && y == 7) || (name.Contains("black") && y == 0))
            {
                PromotionMenu promotionMenu = FindObjectOfType<PromotionMenu>();
                if (promotionMenu != null)
                {
                    promotionMenu.OpenPromotionMenu(this);
                }
            }
        }
    }

    private void Castling(int x, int y)
    {
        controller.GetComponent<Game>().SetPositionEmpty(_xBoard, _yBoard);
        
        SetXBoard(x);
        SetYBoard(y);
        SetCoords();
        _hasMoved = true;
        
        controller.GetComponent<Game>().SetPosition(this.gameObject);
    }

    private void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.04f;
        y *= 1.04f;

        x += -3.64f;
        y += -3.64f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.reference = this.gameObject;
        mpScript.SetCoords(matrixX, matrixY);
    }

    private void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.04f;
        y *= 1.04f;

        x += -3.64f;
        y += -3.64f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.reference = this.gameObject;
        mpScript.SetCoords(matrixX, matrixY);
    }
    
    public List<Vector2> GetPossibleMoves()
    {
        List<Vector2> moves = new List<Vector2>();

        switch (this.name)
        {
            case "white_queen":
            case "black_queen":
                AddLineMoves(moves, 1, 0);
                AddLineMoves(moves, 0, 1);
                AddLineMoves(moves, 1, 1);
                AddLineMoves(moves, -1, 0);
                AddLineMoves(moves, 0, -1);
                AddLineMoves(moves, -1, -1);
                AddLineMoves(moves, -1, 1);
                AddLineMoves(moves, 1, -1);
                break;

            case "white_knight":
            case "black_knight":
                AddLMoves(moves);
                break;

            case "white_bishop":
            case "black_bishop":
                AddLineMoves(moves, 1, 1);
                AddLineMoves(moves, 1, -1);
                AddLineMoves(moves, -1, 1);
                AddLineMoves(moves, -1, -1);
                break;

            case "white_king":
            case "black_king":
                AddSurroundMoves(moves);
                break;

            case "white_rook":
            case "black_rook":
                AddLineMoves(moves, 1, 0);
                AddLineMoves(moves, 0, 1);
                AddLineMoves(moves, -1, 0);
                AddLineMoves(moves, 0, -1);
                break;

            case "white_pawn":
                AddPawnMoves(moves, 1);
                break;

            case "black_pawn":
                AddPawnMoves(moves, -1);
                break;
        }

        return moves;
    }
    
    private void AddLineMoves(List<Vector2> moves, int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();
        int x = _xBoard + xIncrement;
        int y = _yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            moves.Add(new Vector2(x, y));
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            moves.Add(new Vector2(x, y));
        }
    }

    private void AddLMoves(List<Vector2> moves)
    {
        AddMoveIfValid(moves, _xBoard + 1, _yBoard + 2);
        AddMoveIfValid(moves, _xBoard - 1, _yBoard + 2);
        AddMoveIfValid(moves, _xBoard + 2, _yBoard + 1);
        AddMoveIfValid(moves, _xBoard - 2, _yBoard + 1);
        AddMoveIfValid(moves, _xBoard + 1, _yBoard - 2);
        AddMoveIfValid(moves, _xBoard - 1, _yBoard - 2);
        AddMoveIfValid(moves, _xBoard + 2, _yBoard - 1);
        AddMoveIfValid(moves, _xBoard - 2, _yBoard - 1);
    }

    private void AddSurroundMoves(List<Vector2> moves)
    {
        AddMoveIfValid(moves, _xBoard, _yBoard + 1);
        AddMoveIfValid(moves, _xBoard, _yBoard - 1);
        AddMoveIfValid(moves, _xBoard - 1, _yBoard - 1);
        AddMoveIfValid(moves, _xBoard - 1, _yBoard);
        AddMoveIfValid(moves, _xBoard - 1, _yBoard + 1);
        AddMoveIfValid(moves, _xBoard + 1, _yBoard - 1);
        AddMoveIfValid(moves, _xBoard + 1, _yBoard);
        AddMoveIfValid(moves, _xBoard + 1, _yBoard + 1);
    }

    private void AddPawnMoves(List<Vector2> moves, int direction)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(_xBoard, _yBoard + direction) && sc.GetPosition(_xBoard, _yBoard + direction) == null)
        {
            moves.Add(new Vector2(_xBoard, _yBoard + direction));
            
            if (!_hasMoved && sc.PositionOnBoard(_xBoard, _yBoard + 2 * direction) && sc.GetPosition(_xBoard, _yBoard + 2 * direction) == null)
            {
                moves.Add(new Vector2(_xBoard, _yBoard + 2 * direction));
            }
        }
        
        if (sc.PositionOnBoard(_xBoard + 1, _yBoard + direction) && sc.GetPosition(_xBoard + 1, _yBoard + direction) != null && sc.GetPosition(_xBoard + 1, _yBoard + direction).GetComponent<Chessman>().player != player)
        {
            moves.Add(new Vector2(_xBoard + 1, _yBoard + direction));
        }
        if (sc.PositionOnBoard(_xBoard - 1, _yBoard + direction) && sc.GetPosition(_xBoard - 1, _yBoard + direction) != null && sc.GetPosition(_xBoard - 1, _yBoard + direction).GetComponent<Chessman>().player != player)
        {
            moves.Add(new Vector2(_xBoard - 1, _yBoard + direction));
        }
    }

    private void AddMoveIfValid(List<Vector2> moves, int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y) && (sc.GetPosition(x, y) == null || sc.GetPosition(x, y).GetComponent<Chessman>().player != player))
        {
            moves.Add(new Vector2(x, y));
        }
    }
    
}
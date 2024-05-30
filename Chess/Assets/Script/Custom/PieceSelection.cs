using UnityEngine;
using UnityEngine.UI;

public class PieceSelection : MonoBehaviour
{
    public Button whitePawnButton;
    public Button whiteKnightButton;
    public Button whiteBishopButton;
    public Button whiteQueenButton;
    public Button whiteRookButton;
    public Button whiteKingButton;
    public Button blackPawnButton;
    public Button blackQueenButton;
    public Button blackRookButton;
    public Button blackBishopButton;
    public Button blackKnightButton;
    public Button blackKingButton;
    public Button trashButton;
    
    public string selectedPiece;

    public bool pieceSpawn;
    public bool delete;

    public void Start()
    {
        selectedPiece = null;
    }

    public void DeletePiece()
    {
        AudioManager.Instance.Play("ButtonSound");
        delete = true;
        if (pieceSpawn)
        {
            PieceReset();
        }
    }
    
    public void DeletePieceReset()
    {
        delete = false;
    }

    public void WhitePawnPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_pawn";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackPawnPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_pawn";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void WhiteKnightPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_knight";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackKnightPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_knight";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void WhiteBishopPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_bishop";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackBishopPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_bishop";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void WhiteRookPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_rook";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackRookPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_rook";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void WhiteQueenPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_queen";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackQueenPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_queen";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void WhiteKingPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "white_king";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }
    public void BlackKingPiece(string pieceName)
    {
        AudioManager.Instance.Play("ButtonSound");
        selectedPiece = "black_king";
        pieceSpawn = true;
        if (delete)
        {
            DeletePieceReset();
        }
    }

    public string GetSelectedPiece()
    {
        return selectedPiece;
    }

    public void PieceReset()
    {
        selectedPiece = null;
        pieceSpawn = false;
    }
}


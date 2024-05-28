using UnityEngine;

public static class PieceInitializer
{
    public static GameObject[] InitializeWhitePieces(GameObject chessPiece)
    {
        return new[]
        {
            CreatePiece("white_rook", 0, 0, chessPiece), CreatePiece("white_knight", 1, 0, chessPiece),
            CreatePiece("white_bishop", 2, 0, chessPiece), CreatePiece("white_queen", 3, 0, chessPiece), 
            CreatePiece("white_king", 4, 0, chessPiece), CreatePiece("white_bishop", 5, 0, chessPiece), 
            CreatePiece("white_knight", 6, 0, chessPiece), CreatePiece("white_rook", 7, 0, chessPiece),
            CreatePiece("white_pawn", 0, 1, chessPiece), CreatePiece("white_pawn", 1, 1, chessPiece),
            CreatePiece("white_pawn", 2, 1, chessPiece), CreatePiece("white_pawn", 3, 1, chessPiece), 
            CreatePiece("white_pawn", 4, 1, chessPiece), CreatePiece("white_pawn", 5, 1, chessPiece), 
            CreatePiece("white_pawn", 6, 1, chessPiece), CreatePiece("white_pawn", 7, 1, chessPiece)
        };
    }

    public static GameObject[] InitializeBlackPieces(GameObject chessPiece)
    {
        return new[]
        {
            CreatePiece("black_rook", 0, 7, chessPiece), CreatePiece("black_knight", 1, 7, chessPiece),
            CreatePiece("black_bishop", 2, 7, chessPiece), CreatePiece("black_queen", 3, 7, chessPiece), 
            CreatePiece("black_king", 4, 7, chessPiece), CreatePiece("black_bishop", 5, 7, chessPiece), 
            CreatePiece("black_knight", 6, 7, chessPiece), CreatePiece("black_rook", 7, 7, chessPiece),
            CreatePiece("black_pawn", 0, 6, chessPiece), CreatePiece("black_pawn", 1, 6, chessPiece),
            CreatePiece("black_pawn", 2, 6, chessPiece), CreatePiece("black_pawn", 3, 6, chessPiece), 
            CreatePiece("black_pawn", 4, 6, chessPiece), CreatePiece("black_pawn", 5, 6, chessPiece), 
            CreatePiece("black_pawn", 6, 6, chessPiece), CreatePiece("black_pawn", 7, 6, chessPiece)
        };
    }

    public static GameObject CreatePiece(string pieceName, int x, int y, GameObject chessPiece)
    {
        GameObject obj = Object.Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = pieceName;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }
}

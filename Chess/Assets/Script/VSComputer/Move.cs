using UnityEngine;

public class Move
{
    public readonly GameObject Piece;
    public readonly int StartX;
    public readonly int StartY;
    public readonly int TargetX;
    public readonly int TargetY;
    public GameObject CapturedPiece;

    public Move(GameObject piece, int targetX, int targetY)
    {
        Piece = piece;
        StartX = piece.GetComponent<Chessman>().GetXBoard();
        StartY = piece.GetComponent<Chessman>().GetYBoard();
        TargetX = targetX;
        TargetY = targetY;
        CapturedPiece = null;
    }
}
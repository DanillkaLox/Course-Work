using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public GameObject Piece;
    public int StartX;
    public int StartY;
    public int TargetX;
    public int TargetY;
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
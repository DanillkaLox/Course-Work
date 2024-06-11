using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<ChessPieceData> pieces;
    public string currentPlayer;
}

[System.Serializable]
public class ChessPieceData
{
    public string pieceName;
    public string player;
    public int x;
    public int y;
}

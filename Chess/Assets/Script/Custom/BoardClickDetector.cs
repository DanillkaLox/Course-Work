using UnityEngine;

public class BoardClickDetector : MonoBehaviour
{
    public GameObject controller;

    void OnMouseUp()
    {
        Game gameComponent = controller.GetComponent<Game>();
        if (Camera.main != null)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int boardCoords = ConvertToBoardCoordinates(clickPosition);

            if (gameComponent.PositionOnBoard(boardCoords.x, boardCoords.y))
            {
                if (gameComponent.GetComponent<PieceSelection>().pieceSpawn)
                {
                    string pieceName = gameComponent.GetComponent<PieceSelection>().selectedPiece;
                    string player = pieceName.Contains("white") ? "white" : "black";

                    if (gameComponent.CanAddPiece(pieceName, player))
                    {
                        GameObject newChessman = PieceInitializer.CreatePiece(pieceName, boardCoords.x, boardCoords.y, gameComponent.chessPiece);
                        gameComponent.SetPosition(newChessman);

                        for (int i = 0; i < 16; i++)
                        {
                            if (player == "white" && gameComponent.playerWhite[i] == null)
                            {
                                gameComponent.playerWhite[i] = newChessman;
                                gameComponent.AddPiece(pieceName, newChessman);
                                gameComponent.GetComponent<PieceSelection>().PieceReset();
                                break;
                            }
                            if (player == "black" && gameComponent.playerBlack[i] == null)
                            {
                                gameComponent.playerBlack[i] = newChessman;
                                gameComponent.AddPiece(pieceName, newChessman);
                                gameComponent.GetComponent<PieceSelection>().PieceReset();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Cannot place this piece due to restrictions");
                    }
                }
            }
        }
    }

    private Vector2Int ConvertToBoardCoordinates(Vector3 clickPosition)
    {
        int x = Mathf.FloorToInt((clickPosition.x + 4.16f) / 1.04f);
        int y = Mathf.FloorToInt((clickPosition.y + 4.16f) / 1.04f);

        return new Vector2Int(x, y);
    }
}
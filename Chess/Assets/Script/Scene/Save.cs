using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static void SavePlay(Game game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();
        data.pieces = new List<ChessPieceData>();
        data.currentPlayer = game.GetCurrentPlayer();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject piece = game.GetPosition(i, j);
                if (piece != null)
                {
                    Chessman cm = piece.GetComponent<Chessman>();
                    ChessPieceData pieceData = new ChessPieceData()
                    {
                        pieceName = piece.name,
                        player = cm.player,
                        x = cm.GetXBoard(),
                        y = cm.GetYBoard(),
                    };
                    data.pieces.Add(pieceData);
                }
            }
        }
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/savefile.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
}

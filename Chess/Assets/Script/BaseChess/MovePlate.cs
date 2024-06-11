using UnityEngine;
using UnityEngine.Serialization;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    public GameObject reference;
    
    public int matrixX;
    public int matrixY;

    public bool attack;

    [FormerlySerializedAs("move_plate")] public Sprite movePlate;
    [FormerlySerializedAs("move_plate_attack")] public Sprite movePlateAttack;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = movePlateAttack;
        }
    }

    public void OnMouseUp()
    {
        if (Time.timeScale >= 1)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");

            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

                if (cp.name == "white_king") controller.GetComponent<Game>().Winner("BLACK");
                if (cp.name == "black_king") controller.GetComponent<Game>().Winner("WHITE");

                controller.GetComponent<Game>().RemovePiece(cp);
            }

            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
                reference.GetComponent<Chessman>().GetYBoard());

            controller.GetComponent<Game>().SetPosition(reference);

            reference.GetComponent<Chessman>().DestroyMovePlates();
            
            reference.GetComponent<Chessman>().MovePiece(matrixX, matrixY);

            controller.GetComponent<Game>().NextTurn();
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
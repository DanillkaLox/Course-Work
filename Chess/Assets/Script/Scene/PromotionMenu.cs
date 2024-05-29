using UnityEngine;
using UnityEngine.UI;

public class PromotionMenu : MonoBehaviour
{
    public GameObject promotionMenu;
    public Button queenButton;
    public Button rookButton;
    public Button bishopButton;
    public Button knightButton;
    
    public Sprite whiteQueenSprite;
    public Sprite whiteRookSprite;
    public Sprite whiteBishopSprite;
    public Sprite whiteKnightSprite;
    
    public Sprite blackQueenSprite;
    public Sprite blackRookSprite;
    public Sprite blackBishopSprite;
    public Sprite blackKnightSprite;

    private Game _game;
    private Chessman _pawnToPromote;
    private UIManager _uiManager;

    private void Start()
    {
        _game = FindObjectOfType<Game>();
        _uiManager = FindObjectOfType<UIManager>();

        queenButton.onClick.AddListener(() => PromotePawn("queen"));
        rookButton.onClick.AddListener(() => PromotePawn("rook"));
        bishopButton.onClick.AddListener(() => PromotePawn("bishop"));
        knightButton.onClick.AddListener(() => PromotePawn("knight"));
    }

    public void OpenPromotionMenu(Chessman pawn)
    {
        _pawnToPromote = pawn;
        if (_pawnToPromote.player == "white")
        {
            queenButton.GetComponent<Image>().sprite = whiteQueenSprite;
            rookButton.GetComponent<Image>().sprite = whiteRookSprite;
            bishopButton.GetComponent<Image>().sprite = whiteBishopSprite;
            knightButton.GetComponent<Image>().sprite = whiteKnightSprite;
        }
        else
        {
            queenButton.GetComponent<Image>().sprite = blackQueenSprite;
            rookButton.GetComponent<Image>().sprite = blackRookSprite;
            bishopButton.GetComponent<Image>().sprite = blackBishopSprite;
            knightButton.GetComponent<Image>().sprite = blackKnightSprite;
        }
        promotionMenu.SetActive(true);
        Time.timeScale = 0f;
        _uiManager.LockUI();
    }

    private void PromotePawn(string newPiece)
    {
        int x = _pawnToPromote.GetXBoard();
        int y = _pawnToPromote.GetYBoard();
        string color = _pawnToPromote.name.Contains("white") ? "white" : "black";
        
        _game.SetPositionEmpty(x, y);
        
        string piece = color + "_" + newPiece;
        GameObject newChessman = PieceInitializer.CreatePiece(piece, x, y, _game.chessPiece);
        
        _game.RemovePiece(_pawnToPromote.gameObject);
        
        _game.SetPosition(newChessman);

        promotionMenu.SetActive(false);
        Time.timeScale = 1f;
        _uiManager.UnlockUI();
    }
}

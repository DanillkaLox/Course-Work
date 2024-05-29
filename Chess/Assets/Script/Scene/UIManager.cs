using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CanvasGroup gameCanvasGroup;

    public void LockUI()
    {
        gameCanvasGroup.interactable = false;
        gameCanvasGroup.blocksRaycasts = false;
    }

    public void UnlockUI()
    {
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
    }
}
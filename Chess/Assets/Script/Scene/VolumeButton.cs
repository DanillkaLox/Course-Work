using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] public Sprite[] buttonSprites;

    [SerializeField] public Image targetButton;
    
    public void VolumeOnOff()
    {
        AudioManager.instance.Play("ButtonSound");
        if (targetButton.sprite == buttonSprites[0])
        {
            AudioListener.volume = 0f;
            targetButton.sprite = buttonSprites[1];
        }
        else
        {
            AudioListener.volume = 1f;
            targetButton.sprite = buttonSprites[0];
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class FontSettings : MonoBehaviour
{
    void Start ()
    {
        GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}

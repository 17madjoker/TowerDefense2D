using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    
    private void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(1);
        
        Fade(0);
    }

    private void OnApplicationQuit()
    {
        Fade(1);
    }

    private void Fade(float crossValue)
    {
        fadeImage.CrossFadeAlpha(crossValue, 1.2f, true);
    }
}

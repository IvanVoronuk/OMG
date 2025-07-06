using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPuzzleWindowView  : BaseWindow
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button closeButton;

    public void Subscribe(Action back)
    {
        closeButton.onClick.AddListener(back.Invoke);
    }

    public void Unsubscribe()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    public void UpdateVisual(Color color, ButtonType  type)
    {
        buttonText.text = type switch
        {
            ButtonType.Free => "Free",
            ButtonType.Reward => "Reward",
            ButtonType.Currecny => "Currecny",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        image.color = color;
    }
}

public enum ButtonType
{
    Free = 0,
    Reward = 1,
    Currecny = 2
}

using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainWindowView : BaseWindow
{
    [SerializeField] private List<MainWindowItem> items;
    
    public void Init()
    {
        foreach (var item in items)
        {
            Color randomColor = new Color(
                Random.value,  
                Random.value,  
                Random.value   
            );
            item.Image.color = randomColor;
        }
    }
    public void Subscreibe(Action<Image> callback)
    {
        foreach (var item in items)
        {
            item.Button.onClick.AddListener(() => callback.Invoke(item.Image));
        }
    }
    
    public void Unsubscreibe()
    {
        foreach (var item in items)
        {
            item.Button.onClick.RemoveAllListeners();
        }
    }
}



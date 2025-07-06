using System;
using UI;
using UI.Navigation;
using UnityEngine;


public class Data
{
    public Color color;
    public ButtonType ButtonType;
    public Data(Color color, ButtonType buttonType)
    {
        this.color = color;
        ButtonType = buttonType;
    }
}

public class PreviewPuzzleWindowController : IWindowController<PreviewPuzzleWindowView> , IWindowData<Data>
{
    private Data data;
    public PreviewPuzzleWindowView Window { get; set; }

    public void Show()
    {
        Window.UpdateVisual(data.color, data.ButtonType);
        Window.Subscribe(Close);
    }
    public void Hide()
    {
        Window.Unsubscribe();
    }
    
    public Type GetWindowType() => typeof(PreviewPuzzleWindowView);
    public void SetWindows(IWindow window)
    {
        Window = (PreviewPuzzleWindowView)window;
    }
    public void SetData(Data data)
    {
         this.data = data;
    }
    
    private void Close()
    {
        Dependcy.windowsManager.Back();
    }
}

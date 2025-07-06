using System;

namespace UI
{
    public interface IWindowController
    {
        void Show();
        void Hide();
        Type GetWindowType();
        
        void SetWindows(IWindow window);
    }
    
    public interface IWindowController<out T> : IWindowController where T : IWindow
    {
        T Window { get; }
    }
}
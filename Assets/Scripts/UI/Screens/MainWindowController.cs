using System;
using UnityEngine.UI;

namespace UI
{
    public class MainWindowController : IWindowController<MainWindowView>
    {
        private MainWindowView _mainWindowView;
        
        public MainWindowView Window { get; set; }

        public void Show()
        {
            Window.Init();
            Window.Subscreibe(Open);
        }
        private void Open(Image obj)
        {
            var values = Enum.GetValues(typeof(ButtonType));
            int index = UnityEngine.Random.Range(0, values.Length);
            Dependcy.windowsManager.Show<PreviewPuzzleWindowController, Data>(new Data(obj.color, (ButtonType)index));
        }

        public void Hide()
        {
            Window.Unsubscreibe();
        }
        
        public Type GetWindowType() => typeof(MainWindowView);
        public void SetWindows(IWindow window)
        {
            Window = (MainWindowView) window;
        }
    }
}

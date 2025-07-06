using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI.Navigation;
using UnityEngine;

namespace UI
{
    public sealed class WindowsManager : IWindowsManager
    {
        private readonly WindowsSettings windowsSettings;
        private readonly INavigationWindowFactory _navigationWindowFactory;

        private readonly Dictionary<Type, BaseWindow> cachedWindows;
        private readonly Dictionary<Type, IWindowController> windowsController;

        private Transform rootTransform;
        private int countOpendWindows;

        public Stack<IWindowController> History { get; }

        public WindowsManager(WindowsSettings windowsSettings, INavigationWindowFactory navigationWindowFactory,
            int countOpenedWindows = 2)
        {
            this.windowsSettings = windowsSettings;
            this._navigationWindowFactory = navigationWindowFactory;

            cachedWindows = new Dictionary<Type, BaseWindow>(countOpenedWindows);
            windowsController = new Dictionary<Type, IWindowController>(countOpenedWindows);
            History = new Stack<IWindowController>(countOpenedWindows);
        }

        public UniTask Initialize(Transform rootTransform)
        {
            this.rootTransform = rootTransform;

            return UniTask.CompletedTask;
        }

        public T Show<T>() where T : IWindowController
        {
            var type = typeof(T);
            var controller = windowsController[type];
            var window = ActivateView(controller.GetWindowType());
            controller.SetWindows(window);
            controller.Show();

            History.Push(controller);

            return (T) controller;
        }

        public T Show<T, TData>(TData data) where T : IWindowController
        {
            var type = typeof(T);
            var controller = windowsController[type];

            if (data != null && controller is IWindowData<TData> windowData)
            {
                windowData.SetData(data);
            }

            return Show<T>();
        }

        public void Hide<T>() where T : IWindowController
        {
            for (var i = 0; i < History.Count; i++)
            {
                if (History.TryPop(out var windowController) == false)
                {
                    break;
                }

                if (TryHide(windowController))
                {
                    return;
                }
            }
        }

        public bool TryGetWindow(Type type, out IWindow window)
        {
            window = default;

            if (!cachedWindows.TryGetValue(type, out var cachedWindow))
            {
                return false;
            }

            window = cachedWindow;

            return true;
        }

        public void RegisterController(IWindowController controller)
        {
            windowsController.Add(controller.GetType(), controller);
        }

        public void UnregisterController(IWindowController controller)
        {
            windowsController.Remove(controller.GetType());
        }

        public void Back()
        {
            if (History.TryPop(out var result))
            {
                TryHide(result);
            }
        }

        private IWindow ActivateView(Type type)
        {
            if (TryGetWindow(type, out var cachedWindow))
            {
                cachedWindow.Self.SetActive(true);
                cachedWindow.Self.transform.SetAsLastSibling();

                return cachedWindow;
            }

            if (!windowsSettings.TryGetWindowPrefab(type, out var prefab))
            {
                Debug.LogError("Cant find prefab of type " + type);

                return null;
            }

            var window = _navigationWindowFactory.InstantiatePrefabForComponent(prefab, rootTransform);
            cachedWindows.Add(type, window);

            return window;
        }

        private bool TryHide(IWindowController windowController)
        {
            var type = windowController.GetType();
            var windowType = windowController.GetWindowType();

            if (!TryGetWindow(windowType, out var cachedWindow))
            {
                return false;
            }

            if (windowController.GetType() != type)
            {
                return false;
            }

            windowController.Hide();
            cachedWindow.Self.SetActive(false);

            return true;
        }

        public void Dispose()
        {
            windowsController.Clear();
        }
    }
}
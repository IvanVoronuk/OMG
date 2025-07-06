using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public interface IWindowsManager
    {
        UniTask Initialize(Transform rootTransform);

        public Stack<IWindowController> History { get; }

        T Show<T>() where T : IWindowController;
        T Show<T, TData>(TData data) where T : IWindowController;

        void Hide<T>() where T : IWindowController;

        void RegisterController(IWindowController controller);
        void UnregisterController(IWindowController controller);

        void Dispose();


        void Back();
    }
}
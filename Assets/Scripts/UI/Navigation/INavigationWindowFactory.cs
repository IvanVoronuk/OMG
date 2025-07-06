using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public interface INavigationWindowFactory
    {
        T InstantiatePrefabForComponent<T>(T windowPrefab, Transform rootTransform) where T : BaseWindow;
    }

    public class NavigationWindowFactory : INavigationWindowFactory
    {
        public T InstantiatePrefabForComponent<T>(T windowPrefab, Transform rootTransform) where T : BaseWindow
        {
            var instance = Object.Instantiate(windowPrefab, rootTransform);
            return instance.GetComponent<T>();
        }
    }
  
}
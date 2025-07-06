using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "WindowsSettings", menuName = "UI/WindowsSettings", order = 5)]
    public sealed class WindowsSettings : ScriptableObject
    {
        private Dictionary<Type, BaseWindow> allWindowsPrefabs;

        [SerializeField]
        private List<BaseWindow> allWindowsList;

        private void OnEnable()
        {
            allWindowsPrefabs = allWindowsList.ToDictionary(x => x.GetType(), x => x);
        }
        
        public bool TryGetWindowPrefab(Type type, out BaseWindow windowPrefab)
        {
            var result = allWindowsPrefabs.TryGetValue(type, out var window);

            windowPrefab = window;

            return result;
        }
    }
}


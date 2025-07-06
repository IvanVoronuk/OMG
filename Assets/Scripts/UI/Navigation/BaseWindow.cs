using UnityEngine;

namespace UI
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public GameObject Self => gameObject;

        public bool IsActive => Self.activeSelf;
    }
}
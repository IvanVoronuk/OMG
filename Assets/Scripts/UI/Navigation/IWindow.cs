using UnityEngine;

namespace UI
{
    public interface IWindow
    {
        GameObject Self { get; }

        bool IsActive { get; }
    }
}
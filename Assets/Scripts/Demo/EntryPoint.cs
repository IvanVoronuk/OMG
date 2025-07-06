using UI;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform rootCanvas;
    [SerializeField] private WindowsSettings windowsSettings;

    private IWindowsManager windowsManager;

    public void Awake()
    {
        InitializeModules();
        windowsManager.Initialize(rootCanvas);

        windowsManager.RegisterController(new MainWindowController());
        windowsManager.RegisterController(new PreviewPuzzleWindowController());
    }

    public void Start()
    {
        windowsManager.Show<MainWindowController>();
    }

    public void OnDestroy()
    {
        Dependcy.Dispose();
    }

    private void InitializeModules()
    {
        var factory = new NavigationWindowFactory();
        windowsManager = new WindowsManager(windowsSettings, factory);

        Dependcy.windowsManager = windowsManager;
    }
}

public static class Dependcy // use DI
{
    public static IWindowsManager windowsManager;

    public static void Dispose()
    {
        windowsManager.Dispose();
        windowsManager = null;
    }
}
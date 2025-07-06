namespace UI.Navigation
{
    public interface IWindowData<in T> : IWindowData
    {
        void SetData(T data);
    }
    
    public interface IWindowData
    {
    }
}
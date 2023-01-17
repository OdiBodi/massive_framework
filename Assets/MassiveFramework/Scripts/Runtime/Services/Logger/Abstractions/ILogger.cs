namespace MassiveCore.Framework.Runtime
{
    public interface ILogger
    {
        void Print(string text);
        void PrintError(string text);
        void PrintWarning(string text);
    }
}

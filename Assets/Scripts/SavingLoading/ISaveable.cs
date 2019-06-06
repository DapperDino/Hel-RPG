namespace Hel.SavingLoading
{
    /// <summary>
    /// Defines any class that can be saved.
    /// </summary>
    public interface ISaveable
    {
        int LoadPriority { get; }
        void Save();
        void Load();
    }
}

namespace Hel.SavingLoading
{
    public interface ISaveable
    {
        int LoadPriority { get; }
        void Save();
        void Load();
    }
}

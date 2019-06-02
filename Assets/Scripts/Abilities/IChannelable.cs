namespace Hel.Abilities
{
    /// <summary>
    /// Defines any item that can be channelled.
    /// </summary>
    public interface IChannelable
    {
        string Name { get; }
        float ChannelDuration { get; }
    }
}
namespace Hel.Interactables
{
    /// <summary>
    /// Defines any entity that can be interacted with.
    /// </summary>
    public interface IInteractable
    {
        string InteractionText { get; }

        void Interact();
    }
}

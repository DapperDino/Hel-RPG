namespace Hel.Interactables
{
    /// <summary>
    /// Defines what happens when an entity is interacted with.
    /// </summary>
    public interface IInteractable
    {
        void StartHover();
        void Interact();
        void EndHover();
    }
}
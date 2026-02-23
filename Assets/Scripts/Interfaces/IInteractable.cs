public interface IInteractable
{
    bool CanInteract();
    void CreateInteractionIcon();
    void Interact(Player player);
    void DestroyInteractionIcon();
}
using System;

public interface IInteractable
{
    event Action OnInteractEvent;
    void Interact();
}
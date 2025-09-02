using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetInteractionText();
    void EnableOutline(bool hit);
}

public interface IDamageable
{
    void TakeDamage(float damage, bool attackPlayer);
}

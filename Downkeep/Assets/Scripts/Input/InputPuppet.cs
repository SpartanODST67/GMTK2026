using UnityEngine;

public abstract class InputPuppet: MonoBehaviour
{
    public abstract void MoveAction(Vector2 moveVector);
    public abstract void JumpAction();
    public abstract void AttackAction();
    public abstract void AltAttackAction();
}

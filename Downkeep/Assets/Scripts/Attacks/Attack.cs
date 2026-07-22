using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public abstract void PerformAttack();
    public abstract void CancelAttack();
}

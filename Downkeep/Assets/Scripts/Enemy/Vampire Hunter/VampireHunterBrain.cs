using UnityEngine;

enum VampireHunterState
{
    Walking,
    Aiming,
    Recovering
}

public class VampireHunterBrain : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] int direction = 1;
    [SerializeField] bool randomizeDirection = true;
    [SerializeField] BoxCollider2D edgeDetection;
    [SerializeField] BoxCollider2D wallDetection;

    [SerializeField] Vector2 walkTimeRange;
    float walkTime = 0;
    [SerializeField] float aimTime;
    [SerializeField] float attackRecoveryTime;

    [SerializeField] VampireHunterState state;
    float timeInState = 0;

    void Start()
    {
        if(randomizeDirection)
            direction = Random.Range(0, 2) == 0 ? -1 : 1;

        walkTime = Random.Range(walkTimeRange.x, walkTimeRange.y);
    }

    void Update()
    {
        switch(state)
        {
            default:
            case VampireHunterState.Walking:
                rb.linearVelocityX = walkSpeed * direction;
                break;
            case VampireHunterState.Aiming:
                AimWeaponState();
                break;
            case VampireHunterState.Recovering:
                RecoverState();
                break;
        }

        EvaluateStateTime();
    }

    public void ChangeWalkDirection()
    {
        direction *= -1;
        edgeDetection.offset = new Vector2(-edgeDetection.offset.x, edgeDetection.offset.y);
        wallDetection.offset = new Vector2(-wallDetection.offset.x, wallDetection.offset.y);
    }

    private void EvaluateStateTime()
    {
        timeInState += Time.deltaTime;

        float expectedTime = state switch
        {
            VampireHunterState.Walking => walkTime,
            VampireHunterState.Aiming => aimTime,
            VampireHunterState.Recovering => attackRecoveryTime,
            _ => 0
        };

        if(timeInState >= expectedTime)
            NextState();
    }

    private void NextState()
    {
        switch(state)
        {
            case VampireHunterState.Walking:
                state = VampireHunterState.Aiming;
                break;
            case VampireHunterState.Aiming:
                Shoot();
                state = VampireHunterState.Recovering;
                break;
            case VampireHunterState.Recovering:
                walkTime = Random.Range(walkTimeRange.x, walkTimeRange.y);
                state = VampireHunterState.Walking;
                break;
        }

        timeInState = 0;
    }

    private void AimWeaponState()
    {
        rb.linearVelocityX = 0;
        Debug.Log("Aiming");
    }

    private void Shoot()
    {
        Debug.Log("Bang");
    }

    private void RecoverState()
    {
        rb.linearVelocityX = 0;
        Debug.Log("Recovering");
    }
}

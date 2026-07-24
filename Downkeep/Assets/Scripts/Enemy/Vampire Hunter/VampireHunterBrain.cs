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
    [SerializeField] BoxCollider2D rbCollider;

    [SerializeField] Vector2 walkTimeRange;
    float walkTime = 0;
    [SerializeField] float aimTime;
    [SerializeField] float attackRecoveryTime;

    [SerializeField] VampireHunterState state;
    float timeInState = 0;

    [SerializeField] GameObject crossbowObject;
    float firingAngle;
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] SpriteRenderer sprite;

    bool isDead = false;

    void Start()
    {
        if(randomizeDirection)
            direction = Random.Range(0, 2) == 0 ? -1 : 1;

        walkTime = Random.Range(walkTimeRange.x, walkTimeRange.y);
    }

    void FixedUpdate()
    {
        if(isDead) return;

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

    public void Die()
    {
        isDead = true;
        crossbowObject.SetActive(false);
        rbCollider.enabled = false;
        edgeDetection.enabled = false;
        wallDetection.enabled = false;
        sprite.enabled = false;
    }

    private void EvaluateStateTime()
    {
        timeInState += Time.fixedDeltaTime;

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
        crossbowObject.SetActive(true);

        Vector3 from = transform.position;
        Vector3 to = PlayerTracker.Instance.gameObject.transform.position;
        Vector2 direction = to - from;
        direction = direction.normalized;

        firingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        crossbowObject.transform.rotation = Quaternion.Euler(0, 0, firingAngle);
    }

    private void Shoot()
    {
        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, firingAngle));
        Debug.Log("Bang");
    }

    private void RecoverState()
    {
        rb.linearVelocityX = 0;
        crossbowObject.SetActive(false);
        Debug.Log("Recovering");
    }
}

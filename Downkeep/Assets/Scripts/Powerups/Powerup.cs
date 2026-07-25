using System.Collections.Generic;
using UnityEngine;

enum PowerupType
{
    Health,
    BonusHealth,
    JumpBoost,
    Garlic
}

public class Powerup : MonoBehaviour
{
    [SerializeField] SerializableDictionary<PowerupType, Sprite> powerupSprites;
    [SerializeField] SpriteRenderer sprite;
    Dictionary<PowerupType, Sprite> powerupSpritesDict;
    PowerupType type;
    [SerializeField] int powerupChance = 100;
    [SerializeField] AnimationCurve bobCurve;
    float time = 0;
    Vector3 startPosition;

    void Awake()
    {
        if(Random.Range(0, 100) >= powerupChance) Destroy(gameObject);
        startPosition = transform.position;
        powerupSpritesDict = powerupSprites.ToDict();
    }

    void Start()
    {
        type = Random.Range(0, 4) switch
        {
            0 => PowerupType.Health,
            1 => PowerupType.JumpBoost,
            2 => PowerupType.BonusHealth,
            3 => PowerupType.Garlic,
            _ => PowerupType.JumpBoost
        };

        sprite.sprite = powerupSpritesDict[type];
    }

    void Update()
    {
        time += Time.deltaTime;
        time %= bobCurve.keys[^1].time;
        
        var pos = startPosition;
        pos.y += bobCurve.Evaluate(time);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(type)
        {
            case PowerupType.Health:
                HealthPowerUp(collision);
                break;
            case PowerupType.BonusHealth:
                BonusHealthPowerUp(collision);
                break;
            case PowerupType.Garlic:
                GarlicPowerUp(collision);
                break;
            default:
            case PowerupType.JumpBoost:
                JumpBoostPowerUp(collision);
                break;
        }

        Destroy(gameObject);
    }

    private void HealthPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            NotificationManager.Instance.Notification("<color=green>Yum!</color> Feeling better now!");
            health.Heal();
        }        
    }

    private void BonusHealthPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            NotificationManager.Instance.Notification("<color=green>Scrumptious!</color> Feeling even stronger!");
            health.AddMaxHealth(1);
        }        
    }

    private void GarlicPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            NotificationManager.Instance.Notification("<color=red>Yuck!</color> Why did I eat that?");
            Scorekeeper.Instance.AddScore(-10, "Eating <color=red>Garlic</color>");
            health.Hurt();
        }        
    }

    private void JumpBoostPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerPuppet player))
        {
            NotificationManager.Instance.Notification("<color=green>Friends!</color> They've come to assist me!");
            player.maxInAirJumps++;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

enum PowerupType
{
    Health,
    BonusHealth,
    JumpBoost
}

public class Powerup : MonoBehaviour
{
    [SerializeField] SerializableDictionary<PowerupType, Sprite> powerupSprites;
    [SerializeField] SpriteRenderer sprite;
    Dictionary<PowerupType, Sprite> powerupSpritesDict;
    PowerupType type;

    void Awake()
    {
        powerupSpritesDict = powerupSprites.ToDict();
    }

    void Start()
    {
        type = Random.Range(0, 3) switch
        {
            0 => PowerupType.Health,
            1 => PowerupType.JumpBoost,
            2 => PowerupType.BonusHealth,
            _ => PowerupType.JumpBoost
        };

        sprite.sprite = powerupSpritesDict[type];
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
            health.Heal();
        }        
    }

    private void BonusHealthPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            health.AddMaxHealth(1);
        }        
    }

    private void JumpBoostPowerUp(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerPuppet player))
        {
            player.maxInAirJumps++;
        }
    }
}

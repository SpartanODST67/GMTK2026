using UnityEngine;

public class WhipAttackSprite : MonoBehaviour
{
    [SerializeField] Sprite coiledWhip;
    [SerializeField] Sprite attackWhip;
    [SerializeField] Sprite recoveryWhip;

    [SerializeField] SpriteRenderer coiledSpriteRender;
    [SerializeField] SpriteRenderer attackSpriteRender;

    public void ShowCoiled()
    {
        attackSpriteRender.enabled = false;
        coiledSpriteRender.enabled = true;
    }

    public void ShowAttack()
    {
        coiledSpriteRender.enabled = false;
        attackSpriteRender.enabled = true;
        attackSpriteRender.sprite = attackWhip;
    }

    public void ShowRecovery()
    {
        coiledSpriteRender.enabled = false;
        attackSpriteRender.enabled = true;
        attackSpriteRender.sprite = recoveryWhip;
    }

    public void HideWhip()
    {
        coiledSpriteRender.enabled = false;
        attackSpriteRender.enabled = false;
    }
}

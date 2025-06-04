using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 colliderBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        colliderBoxSize = colliderBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)(controllor.LookDirection) * colliderBoxSize.x, 
            colliderBoxSize, 0, Vector2.zero, 0, target);

        if(hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-power);
                if (IsOnKnockback)
                {
                    BaseControllor controllor = hit.collider.GetComponent<BaseControllor>();
                    if(controllor != null)
                    {
                        controllor.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }

    public override void Rotate(bool isLeft)
    {
        if (isLeft) transform.eulerAngles = new Vector3(0, 180, 0);
        else transform.eulerAngles = new Vector3(0, 0, 0);


    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector2 boxCenter = transform.position + (Vector3)(controllor.LookDirection) * colliderBoxSize.x;
        Vector2 size = colliderBoxSize;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCenter, size);
    }
}

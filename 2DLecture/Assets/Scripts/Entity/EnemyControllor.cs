using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : BaseControllor
{
    private EnemyManager enemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        if(distance <= followRange)
        {
            lookDirection = direction;

            if(distance < weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, direction, weaponHandler.AttackRange * 1.5f,
                    (1<<LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                movementDirection = Vector2.zero;
                return;
            }

            movementDirection = direction;
        }
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector2 start = transform.position;
        Vector2 direction = lookDirection;
        float distance = weaponHandler.AttackRange * 1.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, start + direction.normalized * distance);
    }
}

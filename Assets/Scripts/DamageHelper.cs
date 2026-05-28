using UnityEngine;

public static class DamageHelper
{
    public static void ApplyDamage(GameObject target, float damage)
    {
        if (target == null) return;

        EnemyAI meleeEnemy = target.GetComponent<EnemyAI>();
        if (meleeEnemy != null)
        {
            meleeEnemy.TakeDamage(damage);
            return;
        }

        RangedEnemyAI rangedEnemy = target.GetComponent<RangedEnemyAI>();
        if (rangedEnemy != null)
        {
            rangedEnemy.TakeDamage(damage);
        }
    }
}

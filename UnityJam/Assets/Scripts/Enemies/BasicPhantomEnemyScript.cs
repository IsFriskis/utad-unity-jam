using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class BasicPhantomEnemyScript : BasicEnemyScript
    {
        [SerializeField]
        protected BasicPhysicalEnemyScript partner;

        protected override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}
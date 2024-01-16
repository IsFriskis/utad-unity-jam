using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class BasicPhantomEnemyScript : BasicEnemyScript
    {
        [SerializeField]
        protected BasicPhysicalEnemyScript partner;

        public override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}
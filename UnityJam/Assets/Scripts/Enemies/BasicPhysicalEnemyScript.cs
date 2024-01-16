using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class BasicPhysicalEnemyScript : BasicEnemyScript
    {
        [SerializeField]
        protected BasicPhantomEnemyScript partner;

        public override void Die()
        {
            if(partner.currentHealth <= 0 && currentHealth <= 0)
            {
                //Logica de morir // Destroy
            }

        }
    }
}
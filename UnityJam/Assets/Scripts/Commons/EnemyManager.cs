using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyOne;
    [SerializeField]
    public GameObject enemyTwo;
    void Start()
    {
       BasicEnemyScript scriptEnemyOne = enemyOne.GetComponent<BasicEnemyScript>();
       int currentHealthEnemyOne = scriptEnemyOne.currentHealth;
       BasicEnemyScript scriptEnemyTwo = enemyOne.GetComponent<BasicEnemyScript>();
       int currentHealthEnemyTwo = scriptEnemyTwo.currentHealth;
    }

    void Update()
    {

        
    }
}

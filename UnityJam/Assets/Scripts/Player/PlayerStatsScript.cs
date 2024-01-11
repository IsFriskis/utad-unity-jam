using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    [SerializeField]
    public int playerHealth;
    [SerializeField]
    public int playerDamage;
    [SerializeField]
    public int playerMana;
    [SerializeField]
    public bool IsDead;
    
    public int playerHealthMax;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        if (IsDead)
        {
            anim.SetTrigger("Is_Dead");
        }
    }
}

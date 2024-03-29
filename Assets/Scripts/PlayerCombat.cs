﻿
using UnityEngine;

//This script allows for the player to attack enemies

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Transform kickAttackPoint; //Transform is a Component of a GameObject that stores lots of information inc. position, rotation
    public Transform punchAttackPoint;
    public float attackRange = 0.5f; //0.5f = 0.5 float
    public LayerMask enemyLayers;
    public float kickAttackRate = 4f;
    public float punchAttackRate = 2f;
    float nextAttackTime = 0f;
 

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton2))//When "x" button pressed on xbox controller
            {
                Punch();
                nextAttackTime = Time.time + 1f / punchAttackRate;
            }
        }
 
    }
    //this method is called when the player wants the character to do the punch attack (x button)
    void Punch()
    {
        // Play an attack animation
        anim.SetTrigger("Punch");

        //Detect enemies in range of attack
        //"Collider" data type is just unity components thta provide collision detection
        Collider[] hitEnemies = Physics.OverlapSphere(punchAttackPoint.position, attackRange, enemyLayers); // creates 2d array of any objects on enemyLayer inside attack range 

        //Damage those enemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(35);
        }


    }
    //all of this just helps visualise the players attack range inside of the unity editor, can be removed when finished
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(punchAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(kickAttackPoint.position, attackRange);
    }


}


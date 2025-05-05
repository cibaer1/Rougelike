using System;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator anim;
    GameObject player;
    float distToPlayer;
    PlayerMovement playerScript;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] HealthUIEnemy healthUI;
    [SerializeField] PatternSpawner patternScript;
    [SerializeField] float iFrames;
    [SerializeField] GameObject bulletSpawner;
    

    [SerializeField] int phase;

    float snapshotTime;

    
    enum PlayerState
    {
        Idle,
        playerClose,
        playerFar
    }
    enum BossState
    {
        aggresive,
        save,
        desperate

    }

    [SerializeField] BossState bossState;
    [SerializeField] PlayerState playerState;

    void Start()
    {
        
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        phase = 0;
        patternScript.rotating(transform.position, 40f, 5f, 0.1f, 20);
        
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        changeState();
        statebBehavior();
        
    }
    public void hit(float damage)
    {
        if(Time.time > snapshotTime)
        {
            snapshotTime = Time.time + iFrames;
            anim.SetTrigger("Hit");
            if (distToPlayer < 2)
            {
                playerScript.addRegenByMonster();
            }
            takeDamage(damage);
        }
        
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthUI.updateHealth(health, maxHealth);
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    #region state

    void changeState()
    {
        if(health / maxHealth < 0.25f) { phase = 3; }
        else if(health / maxHealth < 0.5f) { phase = 2; }
        else if(health / maxHealth < 0.75f) { phase = 1; }


        if (distToPlayer < 5)
        { playerState = PlayerState.playerClose; }
        else if (distToPlayer > 10)
        { playerState = PlayerState.playerFar; }
    }

    void statebBehavior()
    {
        
    }

    #endregion

}

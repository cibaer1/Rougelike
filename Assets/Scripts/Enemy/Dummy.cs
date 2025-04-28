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
    [SerializeField] float iFrames;
    [SerializeField] GameObject bulletSpawner;

    float snapshotTime;
    void Start()
    {
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        
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

}

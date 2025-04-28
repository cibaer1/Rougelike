using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    HealthUI healthUI;
    public bool isAttacking;
    [SerializeField] int maxHealth;
    [SerializeField] int health;
    [SerializeField] int maxRegenBar;
    [SerializeField] int regenBarDecay;
    [SerializeField] int regenBar;
    [SerializeField] float speed;
    [SerializeField] Transform model;
    [SerializeField] float teleportDist;
    [SerializeField] float healthRegenDelay;
    [SerializeField] int teleportCost;
    [SerializeField] int regenGainedOnMonsterHit;
    [SerializeField] float iFrames;
    Vector3 dir;
    Vector3 thisPos = new Vector3(0, 0, 0);
    private float snapshotTime;
    [SerializeField] bool debug;

    Vector2 input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthUI = GameObject.Find("GameManager").GetComponent<HealthUI>();

        StartCoroutine(healthRegen());
    }
    void getPrevPos()
    {
        Vector3 prevPos = thisPos;
        thisPos = this.transform.position;
        if (prevPos != transform.position)
        {
            dir = (transform.position - prevPos).normalized;
        }
    }
    // Update is called once per frame
    void Update()
    {
        getPrevPos();
        CalculateMovement();
        teleportCheck();
        if(debug && Input.GetKeyDown("r")) { takeDamage(2); }
        
    }
    #region movement
    void CalculateMovement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        if (input.x < 0) { model.localScale = new Vector3(-1, 1, 1); }         //flip sprite
        else if (input.x > 0) { model.localScale = new Vector3(1, 1, 1); }

        rb.AddForce(input * speed);
    }
    void teleportCheck()
    {
        if(Input.GetButtonDown("Jump") && regenBar - teleportCost >= 0)
        {
            regenBar -= teleportCost;
            healthUI.updateRegenBar(regenBar, maxRegenBar);
            transform.position += dir * teleportDist;
        }
    }

    #endregion

    #region health and damage
    
    IEnumerator healthRegen()
    {
        while(true)
        {
            yield return new WaitForSeconds(healthRegenDelay);
            if(health != maxHealth && regenBar - regenBarDecay >= 0)
            {
                regenBar -= regenBarDecay;
                healthUI.updateRegenBar(regenBar, maxRegenBar);
                heal(regenBarDecay);
            }

        }
    }
    void death()
    {
        Destroy(this.gameObject);
    }
    public void addRegen(int regen)
    {
        regenBar += regen;
        regenBar = Mathf.Clamp(regenBar, 0, maxRegenBar);
        healthUI.updateRegenBar(regenBar, maxRegenBar);
    }
    public void addRegenByMonster()
    {
        regenBar += regenGainedOnMonsterHit;
        regenBar = Mathf.Clamp(regenBar, 0, maxRegenBar);
        healthUI.updateRegenBar(regenBar, maxRegenBar);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        float a = health;
        float b = maxHealth;
        healthUI.updateHealth(a, b);
        if(health <= 0) { death(); }
    }
    public void heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        float a = health;
        float b = maxHealth;
        healthUI.updateHealth(a, b);
    }

    public void collision(Collider2D other)
    {
        if (other.tag == "Bullet" && Time.time > snapshotTime)
        {
            snapshotTime = Time.time + iFrames;
            Bullet bulletScript = other.GetComponent<Bullet>();
            takeDamage(bulletScript.damage);
            Destroy(other.gameObject);
        }
    }
    #endregion
}

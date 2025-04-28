using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    [SerializeField] float damage;
    [SerializeField] PlayerMovement playerScript;

    private void Start()
    {
        
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Dummy>().hit(damage);
        }
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }
}

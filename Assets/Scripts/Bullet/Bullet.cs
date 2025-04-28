using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float scale;
    public float survivalTime;
    public Vector3 Targetdir;
    public int damage;
    float initTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initTime = Time.time;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - initTime > survivalTime) { Destroy(this.gameObject); }
        transform.position += Targetdir * speed * Time.deltaTime;
    }
}

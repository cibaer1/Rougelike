using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    GameObject _bulletParent;
    public float survivalTime;
    public float cooldown;
    public float bulletSpeed;
    public float bulletScale;
    public int bulletDamage;
    public float bulletSurvivalTime;
    public float rotationSpeed;

    Bullet bullet;
    void Start()
    {
        _bulletParent = GameObject.FindWithTag("BulletHolder");
        StartCoroutine(survival());
        StartCoroutine(StaticShoot());
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, rotationSpeed) * Time.deltaTime;
    }
    IEnumerator StaticShoot()
    {
        while (true)
        {
            GameObject thisBullet = Instantiate(_bullet, this.transform.position, this.transform.rotation, _bulletParent.transform);
            bullet = thisBullet.GetComponent<Bullet>();
            Vector3 dir = (transform.rotation * Vector3.right);

            bullet.Targetdir = dir;
            bullet.speed = bulletSpeed;
            bullet.scale = bulletScale;
            bullet.damage = bulletDamage;
            bullet.survivalTime = bulletSurvivalTime;

            yield return new WaitForSeconds(cooldown);
        }
    }
    IEnumerator survival()
    {
        yield return new WaitForSeconds(survivalTime);
        Destroy(this.gameObject);
    }
}

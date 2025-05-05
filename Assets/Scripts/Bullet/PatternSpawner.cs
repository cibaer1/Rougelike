using Unity.VisualScripting;
using UnityEngine;

public enum pattern
{
    rotating,
    circle
}


public class PatternSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawner;
    public void spawnPattern(pattern Pattern, float initialRotation, float bulletSpeed, float bulletSize, float bulletCooldown, float rotationSpeed)
    {
        switch(Pattern)
        {
            case pattern.rotating:
                break;
            case pattern.circle:
                break;
        }
    }

    public void rotating(Vector3 targetPos, float rotationSpeed, float bulletSpeed, float cooldown, int dmg)
    {
        spawnSpawner(targetPos, Quaternion.Euler(0, 0, 0), 15f, cooldown, dmg, rotationSpeed, 1f);
        spawnSpawner(targetPos, Quaternion.Euler(0, 0, 180), 15f, cooldown, dmg, rotationSpeed, 1f);
    }
    void spawnSpawner(Vector3 position, Quaternion iniRotation, float survivalTime, float cooldown, int damage, float rotationSpeed, float scale)
    {
        GameObject bullet = Instantiate(spawner, position, iniRotation);
        BulletSpawner script = bullet.GetComponent<BulletSpawner>();
        script.survivalTime = survivalTime;
        script.cooldown = cooldown;
        script.bulletDamage = damage;
        script.rotationSpeed = rotationSpeed;
        script.bulletScale = scale;

    }
}

using UnityEngine;

public enum pattern
{
    rotating,
    circle
}

public class PatternSpawner : MonoBehaviour
{
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
}

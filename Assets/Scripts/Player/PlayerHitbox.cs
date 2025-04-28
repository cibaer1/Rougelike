using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.collision(other);
    }
}

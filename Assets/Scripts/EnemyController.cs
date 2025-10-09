using UnityEngine;

public class EnemyFlee : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float fleeDistance = 5f; // Distance at which enemy starts fleeing
    public float catchDistance = 1f; // Distance at which player catches the enemy
    public float moveSpeed = 3f; // Speed at which enemy moves away
    public float stopDistance = 10f; // Distance at which enemy stops fleeing

    private bool isFleeing = false;

    void Start()
    {
        // Find the player object if not assigned in Inspector
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log($"EnemyFlee on {gameObject.name}: Player found with tag 'Player' at position {player.position}.");
            }
            else
            {
                Debug.LogError($"EnemyFlee on {gameObject.name}: No GameObject with tag 'Player' found. Please ensure the player GameObject has the 'Player' tag or assign the Player Transform manually in the Inspector.");
            }
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning($"EnemyFlee on {gameObject.name}: Player Transform is null. Skipping update.");
            return; // Skip update if player is not assigned
        }

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log($"EnemyFlee on {gameObject.name}: Distance to player = {distanceToPlayer:F2}, Catch Distance = {catchDistance:F2}");

        // Check if player is within catch distance
        if (distanceToPlayer < catchDistance)
        {
            // Enemy caught - notify GameManager and destroy
            Debug.Log($"EnemyFlee on {gameObject.name}: Caught by player at distance {distanceToPlayer:F2}!");
            GameManager.Instance?.EnemyCaught();
            Destroy(gameObject);
            return;
        }

        // Check if player is within flee distance
        if (distanceToPlayer < fleeDistance)
        {
            isFleeing = true;
        }
        // Stop fleeing if player is far enough
        else if (distanceToPlayer > stopDistance)
        {
            isFleeing = false;
        }

        // Move away from player if fleeing
        if (isFleeing)
        {
            // Calculate direction away from player
            Vector3 fleeDirection = (transform.position - player.position).normalized;

            // Move enemy in flee direction
            transform.position += fleeDirection * moveSpeed * Time.deltaTime;

            // Optional: Make enemy face the direction it's moving
            if (fleeDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(fleeDirection);
            }
        }
    }

    // Optional: Visualize flee, catch, and stop distances in editor
    void OnDrawGizmosSelected()
    {
        // Draw catch distance sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, catchDistance);

        // Draw flee distance sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);

        // Draw stop distance sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
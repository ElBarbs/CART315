using System;
using UnityEngine;
using UnityEngine.AI; // Necessary for NavMesh functionalities

public class Player2DNavMeshMovement : MonoBehaviour
{
    public float speed = 5.0f; // Player movement speed
    public float maxNavMeshDistance = 1.0f; // Max distance from the NavMesh to consider valid for movement

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get horizontal input (A and D keys)
        float vertical = Input.GetAxis("Vertical"); // Get vertical input (W and S keys)

        Vector3 direction = new Vector3(horizontal, vertical, 0); // Construct the movement direction vector

        if (direction.magnitude >= 0.1f) // Check if there's significant movement input
        {
            TryMove(direction); // Attempt to move in the desired direction
        }
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * (speed * Time.deltaTime); // Calculate the target position based on the direction and speed

        // Use NavMesh.SamplePosition to check if the target position is valid (within NavMesh boundaries)
        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, maxNavMeshDistance, NavMesh.AllAreas))
        {
            transform.position = hit.position; // If valid, move the player to the nearest point on the NavMesh
        }
        // If the position isn't valid, the player won't move. You can add handling here if needed.
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(other.gameObject);
            GameManager.Instance.UpdateMeter("Trash", 0.5f);
        }
    }
}

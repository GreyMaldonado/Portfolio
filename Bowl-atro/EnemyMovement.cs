using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // player to chase
    private NavMeshAgent navMeshAgent; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // If the player exists then chase after it
        if(player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}

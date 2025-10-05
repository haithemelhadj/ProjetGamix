using UnityEngine;
using UnityEngine.AI;

public class AiFollowPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public float followDistance = 5f;
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
            agent.SetDestination(player.position);
        if(Vector3.Distance(this.transform.position,player.position)>followDistance)
        {
        }
    }
}

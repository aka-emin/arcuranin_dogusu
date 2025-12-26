using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;        // Takip edilecek obje
    public float followDistance = 145f;
    public float unfollowDistance = 15f;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // 🔒 2D için ZORUNLU ayarlar
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (target == null || !agent.isOnNavMesh) return;

        float distance = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(target.position.x, target.position.y)
        );

        // Belirli mesafedeyse takip et
        if (distance <= followDistance&& distance>=unfollowDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
        } 
        HandleSpriteDirection();
    }

    // 🔒 NavMeshAgent ne yaparsa yapsın sprite dik kalsın
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
       

    }
    void HandleSpriteDirection()
    {
        if (agent.velocity.x > 0.05f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (agent.velocity.x < -0.05f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

}

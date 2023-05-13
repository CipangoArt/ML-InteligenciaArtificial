using Panda;
using UnityEngine;
using UnityEngine.AI;



public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] AIBehaviour player;
    Vector3 playerDir;
    [SerializeField] LayerMask layerMask;
    Vector3 lastPlayerPos;
    [SerializeField] NavMeshAgent enemyAgent;
    bool awareOfPlayer;



    [Task]
    bool IsMoving()
    {
        bool moving = enemyAgent.velocity == Vector3.zero ? false : true;
        return moving;
    }

    [Task]
    bool isAware()
    {
        return awareOfPlayer;
    }

    [Task]
    bool BecomeUnaware()
    {
        awareOfPlayer = false;
        return true;
    }


    [Task]
    bool PatrolArea()
    {
        Vector3 RandomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        enemyAgent.SetDestination(RandomDir.normalized * 10 + transform.position );
        return true;
    }

    [Task]
    bool MustHaveBeenTheWind()
    {
        awareOfPlayer = false;
        Vector3 RandomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        enemyAgent.SetDestination(RandomDir.normalized * 1 + transform.position );
        return false;
    }

    [Task]
    bool ChasePlayer()
    {
        enemyAgent.SetDestination(lastPlayerPos);
        return true;
    }


    [Task]
    bool CanSeePlayer()
    {
        return PlayerOnVision();
    }

    [Task]

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AIBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = player.transform.position - transform.position;
    }


    bool PlayerOnVision()
    {
        RaycastHit playerHit;
        if (Physics.Raycast(transform.position, playerDir.normalized, out playerHit))
        {
            if (playerHit.collider.CompareTag("Player"))
            {
                awareOfPlayer = true;
                lastPlayerPos = player.transform.position;
                return true;
            }
           
        }

        return false;

    }
}

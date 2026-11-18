using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlime : MonoBehaviour
{ 
    [SerializeField]private gameManager gameManager;
    
    private Animator anim;

    [SerializeField] private int hp;
    private bool isDie;

    [SerializeField] private float Corroutinetime;

    [SerializeField] private EnemyState state;

    [SerializeField] private const float idleWaitTme = 3f;
    [SerializeField] private const float patrolWaitTme = 5f;
    
    [Header("AI Details")]
    private NavMeshAgent agent;
    private Vector3 destination;
    private int idWayPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindAnyObjectByType(typeof(gameManager)) as gameManager;
        
        changesState(state);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GetHit(int amount)
    {
        if (isDie)
        {
            return;
        }
        
        
        hp -= amount;

        if (hp > 0)
        {
            anim.SetTrigger("getHit");
        }

        else
        {
             anim.SetTrigger("die");
             StartCoroutine(Died());
        }
        
        
        
    }

    private IEnumerator Died()
    {
        isDie = true;
        yield return new WaitForSeconds(1.15f);
        Destroy(gameObject);
    }

    private void StateManeger()// responsavel por informar o que vai acontecer em cada estado.
    {
        switch (state)
        {
            case EnemyState.IDLE:
                destination = transform.position;
                agent.destination = destination;
                StartCoroutine(IdleRoutine());
                break;
            case EnemyState.ALERT:
                break;
            case EnemyState.EXPLORE:
                break;
            case EnemyState.FOLLOW:
                break;
            case EnemyState.FURY:
                break;
            case EnemyState.PATROL:
                idWayPoint = Random.Range(0, gameManager.slimeWayPoints.Length);
                destination = gameManager.slimeWayPoints[idWayPoint].position;
                agent.destination = destination;
                StartCoroutine(PatrolRoutine());
                break;
        }
    }

    private void changesState(EnemyState newState) // responsavel por mudar de estado.
    {
        StopAllCoroutines();
        
        state = newState;
        
        switch (state)
        {
            case EnemyState.IDLE:
                StartCoroutine(IdleRoutine());
                break;
            case EnemyState.ALERT:
                break;
            case EnemyState.PATROL:
                StartCoroutine(PatrolRoutine());
                break;
        }
    }

    private IEnumerator IdleRoutine()
    {
        yield return new WaitForSeconds(idleWaitTme);
        
        if (Random.Range(0, 100) < 50)
        {
            changesState(EnemyState.IDLE);
        }
        else
        {
            changesState(EnemyState.PATROL);
        }
        
    }

    private IEnumerator PatrolRoutine()
    {
         yield return new WaitForSeconds(patrolWaitTme);

         changesState(EnemyState.IDLE);
    }
}   

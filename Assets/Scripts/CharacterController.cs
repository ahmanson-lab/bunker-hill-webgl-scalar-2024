using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState
{
    Idle,
    Wandering,
    GoingHome,
}

public class CharacterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public CharacterState state;
    public Transform[] wanderPoints;
    public Transform home;
    public float exitIdleTime;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetState(CharacterState newState)
    {
        state = newState;
        switch (state)
        {
            case CharacterState.Idle:
                exitIdleTime = Time.time + Random.Range(1, 5);
                animator.SetBool("isWalking", false);
                break;
            case CharacterState.Wandering:
                agent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length)].position);
                animator.SetBool("isWalking", true);
                break;
            case CharacterState.GoingHome:
                agent.SetDestination(home.position);
                animator.SetBool("isWalking", true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case CharacterState.Idle:
                if (Time.time > exitIdleTime)
                {
                    SetState(CharacterState.Wandering);
                }
                break;
            case CharacterState.Wandering:
                if (agent.remainingDistance < 0.5f)
                {
                    SetState(CharacterState.Idle);
                }
                break;
            case CharacterState.GoingHome:
                if (agent.remainingDistance < 0.5f)
                {
                    SetState(CharacterState.Idle);
                }
                break;
        }
    }
}

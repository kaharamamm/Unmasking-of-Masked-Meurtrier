using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet.Managing.Object;
using FishNet.Object;
using UnityEngine;
using UnityEngine.AI;
 
public class MultiplayerAgent : NetworkBehaviour
{
    private bool isRunning = false, isMoving = false;
    [SerializeField] private List<Transform> positions = new List<Transform>();
 
    private NavMeshAgent _agent;
    private int positionIndex = 0;
    private Animator mAnimator;
    private Vector3 lastPosition;
    private Transform myTransform;

    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;
        Patroling();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
 
    private void Update()
    {
        if (myTransform.position!= lastPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            Walk(); 
        }
        else if (isRunning)
        {
            Run();
        }
        else
        {
            Idle();
        }
    }
    public void Patroling()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3.5f);
        NextPosition();
    }

    public void Idle()
    {
        //Debug.Log("Idle içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Enemy", 0);
        }
    }
    public void Walk()
    {
        //Debug.Log("Walk içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Enemy", 0.5f);
        }
    }
    public void Run()
    {
        //Debug.Log("Run içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Enemy", 1);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void NextPosition()
    {
        if (_agent.SetDestination(positions[positionIndex].position))
        {
            positionIndex++;
            if (positionIndex >= positions.Count)
            {
                positionIndex = 0;
            }
            Patroling();
        }
        Debug.Log("Position index = " + positionIndex);
    }
}
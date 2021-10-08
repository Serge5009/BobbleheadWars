using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;



public class Alien : MonoBehaviour
{
    public UnityEvent OnDestroy;


    public Transform target;
    private NavMeshAgent agent;

    public float navigationUpdate;
    private float navigationTime = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        navigationTime += Time.deltaTime;
        if (navigationTime > navigationUpdate)
        {
            agent.destination = target.position;
            navigationTime = 0;
        }

        //  Console error fix
        if (target != null)
        {
            agent.destination = target.position;
        }
    }

    public void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();

        // All death actions ABOVE this line
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        Die();        //  the game is over for this buddy
    }

}

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
    private DeathParticles deathParticles;

    public Rigidbody head;
    public bool isAlive = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (isAlive)
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
    }

    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);

        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        head.GetComponent<SelfDestruct>().Initiate();

        if (deathParticles)
        {
            deathParticles.transform.parent = null;
            deathParticles.Activate();
        }

        // All pre death actions go before this line
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);

            Die();  // F
        }
    }

    public DeathParticles GetDeathParticles()
    {
        if (deathParticles == null)
        {
            deathParticles = GetComponentInChildren<DeathParticles>();
        }
        return deathParticles;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public Transform player;
    public float zombieSpeed = 3.0f;
    public float attackRange = 1.0f;
    private NavMeshAgent agent;
    private Animator animator;

    public float attackCooldown = 1.0f;
    private float attackTimer = 0.0f;
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            return;
        }

        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);  // distancia entre o zombie e o jogador

        if (distance < attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("Attacking", true);

            // Verifica se o zombie pode atacar
            if (attackTimer <= 0)
            {
                // Subtrai vida do jogador
                Debug.Log(GameObject.FindWithTag("Player"));
                FPSController playerController = GameObject.FindWithTag("Player").GetComponent<FPSController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(5);
                    attackTimer = attackCooldown;
                }
                else
                {
                    Debug.LogError("FPSController component not found on player.");
                }
            }

        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Attacking", false);
            agent.SetDestination(player.position);
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

}

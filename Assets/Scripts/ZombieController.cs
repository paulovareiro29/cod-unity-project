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
    private Animator animator;  // Novo

    public float attackCooldown = 1.0f;  // Tempo em segundos entre cada ataque
    private float attackTimer = 0.0f;  // Temporizador para rastrear o cooldown

    void Start()
    {
        // Encontra o jogador usando a tag "PlayerModel"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Se o objeto playerObject foi encontrado, pegue seu componente Transform
        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player found");
        }
        else
        {
            Debug.LogError("Player object not found");
            return;
        }

        // Obtém o componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
        }

        // Obtém o componente Animator
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    void Update()
{
        // Verifique se o player, o agent e o animator estão devidamente instanciados
        float distance = Vector3.Distance(player.position, transform.position);  // distancia entre o zumbi e o jogador

        if(distance < attackRange)
        {
            // Aqui adiciona o que o zumbi deve fazer quando estiver no range de ataque
            agent.isStopped = true;
            animator.SetBool("Attacking", true);

            // Verifique se o zumbi pode atacar
            if (attackTimer <= 0)
            {
                // Subtrai vida do jogador
                FPSController playerController = player.gameObject.GetComponent<FPSController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(5);
                    attackTimer = attackCooldown;  // Reseta o temporizador
                }
                else
                {
                    Debug.LogError("FPSController component not found on player.");
                }
            }

            Debug.Log("Zombie atacando o jogador!");
        }
        else
        {
            // Siga o jogador
            agent.isStopped = false;
            animator.SetBool("Attacking", false);  // Novo
            agent.SetDestination(player.position);
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

}

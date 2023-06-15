using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{

    public float health;
    private FPSController player;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<FPSController>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (player != null)
            {
                player.IncreaseScore(100);  // Aumenta o score do jogador em 100 pontos
            }

            Destroy(gameObject);
        }
    }
}
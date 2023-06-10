using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public FPSController player;
    public Text healthText;
    public Text staminaText;

    void Update()
    {
        // Atualiza os textos da vida e stamina para mostrar os valores atuais
        healthText.text = "Health: " + player.currentHealth;
        staminaText.text = "Stamina: " + player.currentStamina;
    }
}
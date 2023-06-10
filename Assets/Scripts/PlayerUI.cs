using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public FPSController player;
    public GunData gunData;
    public Text healthText;
    public Text staminaText;
    public Text ammoText;

    void Update()
    {
        // Atualiza os textos da vida e stamina para mostrar os valores atuais
        healthText.text = "Health: " + player.currentHealth;
        staminaText.text = "Stamina: " + player.currentStamina;
        
        if(gunData.reloading) {
            ammoText.text = "Reloading...";
        } else {
            ammoText.text = "Ammo: " + gunData.currentAmmo + "/" + gunData.totalAmmo;
        }
        
    }
}
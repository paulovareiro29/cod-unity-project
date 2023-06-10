using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{


    public override void onPickup(GameObject player)
    {
        FPSController controller = player.GetComponent<FPSController>();

        controller.currentHealth =  Mathf.Clamp(controller.currentHealth + amount, 0, controller.maxHealth);;
    }
}

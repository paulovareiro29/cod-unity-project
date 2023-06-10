using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{

    public GunData gun;

    public override void onPickup(GameObject player)
    {
        gun.totalAmmo += amount;
    }
}

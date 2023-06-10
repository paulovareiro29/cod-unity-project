using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public int amount;
    public float pickupRate;

    float timeSinceLastPickup;

    private void Update()
    {
        timeSinceLastPickup += Time.deltaTime;
    }

    private bool CanPickup() => timeSinceLastPickup / 60f * 100f > pickupRate;

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (CanPickup())
            {
                Debug.Log("Picked up");
                onPickup(col.gameObject);
                timeSinceLastPickup = 0;
            }
        }

    }
    public abstract void onPickup(GameObject player);

}
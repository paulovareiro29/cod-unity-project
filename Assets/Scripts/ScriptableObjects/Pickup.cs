using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public int amount;
    public float pickupRate;

    float timeSinceLastPickup;

    Renderer renderer;
    Renderer[] childRenderers;

    private void Start() {
        renderer = GetComponent<Renderer>();
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        timeSinceLastPickup += Time.deltaTime;

        renderer.enabled = CanPickup();
        foreach (Renderer r in childRenderers)
        {
            r.enabled = CanPickup();
        }
    }

    private bool CanPickup() => timeSinceLastPickup / 60f * 100f > pickupRate;

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (CanPickup())
            {
                onPickup(col.gameObject);
                timeSinceLastPickup = 0;
            }
        }

    }
    public abstract void onPickup(GameObject player);

}
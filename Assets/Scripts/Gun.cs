using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    private AudioSource audio;
    private AudioSource reloadAudio;

    float timeSinceLastShot;

    private void Start() {
        audio = GetComponent<AudioSource>();
        AudioSource[] childAudioSources =  GetComponentsInChildren<AudioSource>();

        if(childAudioSources.Length > 1) {
            reloadAudio = childAudioSources[1];
        }

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        gunData.currentAmmo = gunData.magSize;
        gunData.totalAmmo = gunData.magSize * 3;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (cam == null)
            return;
        
        if (!gunData.reloading && gunData.currentAmmo < gunData.magSize && gunData.totalAmmo != 0 && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        gunData.reloading = true;

        reloadAudio?.Play();

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToReload = gunData.magSize - gunData.currentAmmo;
        gunData.totalAmmo -= ammoToReload;
        
        if(gunData.totalAmmo < 0) {
            ammoToReload -= -gunData.totalAmmo;
            gunData.totalAmmo = 0;
        }
        
        gunData.currentAmmo = gunData.currentAmmo + ammoToReload;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot() {
        if (cam == null)
            return;

        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);

                    GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(impact, 2f);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() { 
        muzzleFlash?.Play();
        audio?.Play();
     }
}
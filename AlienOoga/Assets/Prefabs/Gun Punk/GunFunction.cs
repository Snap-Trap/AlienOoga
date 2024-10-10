using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunFunction : MonoBehaviour
{
    public InputAction shootAction; //De actie die de speler gebruikt om te schieten
    public InputAction reloadAction; //De actie die de speler gebruikt om te herladen

    public GunScriptable gunScriptable; //Het scriptable object van het wapen
    public GameObject gunBulletPrefab; //Het prefab van de kogel
    public Transform gunBarrel; //De loop van het wapen
    public string gunName; //De naam van het wapen
    public float gunDamage; //De schade van het wapen
    public float gunFireRate; //De vuursnelheid van het wapen
    public int gunBulletCount; //Het aantal kogels dat het wapen kan schieten
    public int gunAmmo; //Het aantal kogels dat het wapen kan dragen

    public float nextFireTime = 0f; //De tijd wanneer het wapen weer kan schieten
    void Start()
    {
        gunName = gunScriptable.gunName; //De naam van het wapen is gelijk aan de naam van het scriptable object
        gunDamage = gunScriptable.damage; //De schade van het wapen is gelijk aan de schade van het scriptable object
        gunFireRate = gunScriptable.fireRate; //De vuursnelheid van het wapen is gelijk aan de vuursnelheid van het scriptable object
        gunBulletCount = gunScriptable.bullets; //Het aantal kogels dat het wapen kan schieten is gelijk aan het aantal kogels dat het scriptable object kan schieten
        gunAmmo = gunScriptable.ammo; //Het aantal kogels dat het wapen kan dragen is gelijk aan het aantal kogels dat het scriptable object kan dragen
        gunBulletPrefab = gunScriptable.BulletPrefab; //Het prefab van de kogel is gelijk aan het prefab van het scriptable object
    }

    // Update is called once per frame
    void Update()
    {
        if (shootAction.ReadValue<float>() == 1 && Time.time >= nextFireTime)
        {
            Shooting();
            nextFireTime = Time.time + 1f / gunFireRate;
        }

        if (reloadAction.ReadValue<float>() == 1)
        {
            if(gunAmmo < gunScriptable.ammo)
            {
                Reload();
            }
        }

    }

    private void Shooting()
    {
        if (gunAmmo > 0)
        {
            var bulletTime = Instantiate(gunBulletPrefab, gunBarrel.transform.position, gunBarrel.transform.rotation); //Maakt een kogel aan op de positie en rotatie van het wapen
            bulletTime.GetComponent<Rigidbody>().AddForce(gunBarrel.transform.forward * 1000f); //Geeft de kogel een kracht naar voren
            gunAmmo--; //Haalt 1 kogel van de totale kogels af
            Destroy(bulletTime, 1.5f); //Verwijderd de kogel na 1.5 seconden
        }
    }
    private void OnEnable()
    {
        shootAction.Enable(); //Zet de actie aan
        reloadAction.Enable(); //Zet de actie aan
    }

    private void OnDisable()
    {
        shootAction.Disable(); //Zet de actie uit
        reloadAction.Disable(); //Zet de actie uit
    }

    private void Reload()
    {
        gunAmmo = gunScriptable.ammo;
    }
}

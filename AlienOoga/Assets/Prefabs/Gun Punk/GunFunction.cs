using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunFunction : MonoBehaviour
{
    public InputAction shootAction; //De actie die de speler gebruikt om te schieten

    public GunScriptable gunScriptable; //Het scriptable object van het wapen
    public GameObject gunBulletPrefab; //Het prefab van de kogel
    public string gunName; //De naam van het wapen
    public float gunDamage; //De schade van het wapen
    public float gunFireRate; //De vuursnelheid van het wapen
    public int gunBulletCount; //Het aantal kogels dat het wapen kan schieten
    public int gunAmmo; //Het aantal kogels dat het wapen kan dragen
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
        if (shootAction.ReadValue<int>() == 1)
        {

        }

    }
    private void OnEnable()
    {
        shootAction.Enable(); //Zet de actie aan
    }

    private void OnDisable()
    {
        shootAction.Disable(); //Zet de actie uit
    }
}

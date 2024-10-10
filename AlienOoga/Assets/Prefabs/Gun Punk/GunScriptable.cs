using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun", menuName = "Guns")]
public class GunScriptable : ScriptableObject
{
    public string gunName; //De naam van het wapen
    public float damage; //De schade van het wapen
    public float fireRate; //De vuursnelheid van het wapen
    public int bullets; //Het aantal kogels dat het wapen kan schieten
    public int ammo; //Het aantal kogels dat het wapen kan dragen
    public GameObject BulletPrefab; //Het prefab van de kogel
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform firePoint;
    public Transform playerDirection;

    public bool canShoot = true;

    public float bulletForce = 20f;
    public float shootCooldown = 2f;


    public void Shoot()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            canShoot = false;
            Destroy(bullet, 2f);
            StartCoroutine(ShootCooldown());
        }
    }

    void Update()
    {
        if (playerDirection != null)
        {
            firePoint.LookAt(playerDirection);
        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
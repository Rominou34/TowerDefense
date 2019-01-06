using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("Attributes")]
    private Transform target;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0.5f;

    [Header("Unity setup fields")]
    private Transform partToRotate;
    public float rotationSpeed = 20f;

    public GameObject bulletPrefab;
    private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        partToRotate = transform.Find("PartToRotate");
        firePoint = partToRotate.Find("FirePoint");

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing if no target
        if (target == null) return;

        // Gets the direction to look at
        Vector3 direction = target.position - transform.position;
        // Converts it into quaternion
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Converts it to rotation using the Euler angles of the quternion (lerp is there to make it smooth)
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        // Set the rotation
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null) {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

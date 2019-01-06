using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public GameObject impactEffect;

    public float speed = 10f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // In case the enemy is destroyed (by reaching the base)
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the distance to the target is less than the distance we have to travel this frame, we hit the target
        if (direction.magnitude <= distanceThisFrame)
        {
            transform.Translate(direction.normalized * direction.magnitude, Space.World);
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        // For now we also destroy the enemy
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}

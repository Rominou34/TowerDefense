using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Material hoverMaterial;
    public Material forbiddenMaterial;
    public GameObject turretPrefab;

    private GameObject turret;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("You can't build there !!");
            return;
        }

        // Build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);

        // Hide the node
        rend.material = forbiddenMaterial;
        rend.enabled = false;
    }

    void OnMouseEnter()
    {
        rend.enabled = true;
        if (turret != null)
        {
            rend.material = forbiddenMaterial;
        } else
        {
            rend.material = hoverMaterial;
        }
    }

    void OnMouseExit()
    {
        rend.enabled = false;
    }
}

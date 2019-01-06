using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Serves as a reference to the BuildManager
    public static BuildManager instance;

    public GameObject testTurretPrefab;

    private GameObject turretToBuild;

    // We say that the only BuildManager is this one
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        turretToBuild = testTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}

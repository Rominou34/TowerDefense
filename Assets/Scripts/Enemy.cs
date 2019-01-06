using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent myNavMeshAgent;
    GameObject baseObject;

    void Start() {
        //myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        if (baseObject == null) {
            baseObject = GameObject.FindWithTag("Base");
            myNavMeshAgent.SetDestination(baseObject.transform.position);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestinationToMousePosition();
        }
    }

    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //myNavMeshAgent.SetDestination(hit.point);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour
{
    //[SerializeField] Transform target;
    NavMeshAgent agent;
    
   // Ray ray;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }


        //Debug.DrawRay(ray.origin, ray.direction*700);

        //agent.destination = target.position;
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit)
        {
            agent.destination = hit.point;
        }
    }
}

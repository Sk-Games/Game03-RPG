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
       if(Input.GetMouseButton(0))
        {
            MoveToCursor();
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }


        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 globalVal = agent.velocity;
        Vector3 localVal = transform.InverseTransformDirection(globalVal);
        float speed = localVal.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
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

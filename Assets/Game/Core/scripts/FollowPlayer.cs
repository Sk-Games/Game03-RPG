using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
   

   
    void LateUpdate()
    {
        this.transform.position = target.position;
    }
}

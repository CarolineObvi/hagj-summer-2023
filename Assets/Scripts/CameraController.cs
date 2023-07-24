using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject target;
    public Transform target;
    public float yOffset = 1f;
    public float FollowSpeed = 2f;

    // Update is called once per frame
    void LateUpdate()
    {
        //this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 newPos = new Vector3(target.position.x,target.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
     public Vector2 boundary = new Vector2(2,2);
     // Use this for initialization
     void Start () {
     
     }
     
     // Update is called once per frame
     void Update () {
     if(Vector3.Distance(transform.position, boundary) < 1.0f && Vector3.Distance(transform.position, -boundary) < 1.0f){
         Debug.Log(gameObject.name + "Is within the Boundary");
     }
     }
 }
 
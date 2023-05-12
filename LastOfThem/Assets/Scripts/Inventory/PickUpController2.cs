using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController2 : MonoBehaviour
{

    public GameObject obj;
    public Transform playerTransform;
    public float radius = 6f;
    public float go;
    public Camera camera;
    int layerMask = 6;

            RaycastHit hit;

    void Start()
    {

    }

    private void Update() {
        {
        //    if(Physics.Raycast(gameObject.transform.position, transform.TransformDirection(-Vector3.up), out hit, range))
        //     {
        //             Debug.Log(hit.collider.name);
        //     }
                
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius, layerMask);

        Debug.Log(hitColliders.Length);


        }
    }

    void PickUp()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    public float rotationSpeed=1; //cannon rotation
    public float blastPower=5;

    public GameObject Cannonball;
    public Transform ShotPoint;

    public GameObject Explosion;

    // Update is called once per frame
    void Update()
    {
        
        //Rotate attached object (cannon)
        float HorizontalRotation = Input.GetAxis("Horizontal");
        float VerticalRotation = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(0,HorizontalRotation * rotationSpeed, VerticalRotation* rotationSpeed));
        
        if (Input.GetKeyDown(KeyCode.Space)){

        }


    }
}

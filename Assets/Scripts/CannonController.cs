using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CannonController : MonoBehaviour
{


    float minAngle=4;
    float maxAngle=75;
    public float rotationSpeed=1; //cannon rotation
    public float blastPower=2;

    public GameObject Cannonball;
    public Transform ShotPoint;

    //public GameObject Explosion;

    // Update is called once per frame
    void Update()
    {
        
        //Rotate attached object (cannon)
        float HorizontalRotation = Input.GetAxis("Horizontal");
        float VerticalRotation = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(0,HorizontalRotation * rotationSpeed, VerticalRotation* rotationSpeed));
        
        float CannonAngle = transform.eulerAngles.z;
        if (CannonAngle >= 0 && CannonAngle <= 45){
            CannonAngle += 45;
        } else if (CannonAngle <= 360 && CannonAngle >= 315) {
            CannonAngle -= 315;
        }
        Debug.Log(CannonAngle);


        if ((CannonAngle <= minAngle)) {
            transform.Rotate(0,0,1);
        } else if ((CannonAngle >= maxAngle)) {
            transform.Rotate(0,0,-1);
        }




        //Shoot cannonball
        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject CreatedCannonBall = Instantiate(Cannonball,ShotPoint.position,ShotPoint.rotation);
            CreatedCannonBall.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * blastPower;
        }


    }
}

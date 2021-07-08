using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using T5Input = TiltFive.Input;

public class CannonController : MonoBehaviour
{

    //Cannon setup vars
    float minAngle=4;
    float maxAngle=85;
    public float rotationSpeed=1;
    public float blastPower=30f;

    public GameObject Cannonball;
    public Transform ShotPoint;

    bool canshoot = true;

    //TODO: public GameObject Explosion and sound

    void Update()
    {
      
        //Rotate attached object (cannon)
        float HorizontalRotation = Input.GetAxis("Horizontal");
        float VerticalRotation = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(0,HorizontalRotation * rotationSpeed, VerticalRotation* rotationSpeed));
        
        //fix cannonangle to 0=UP and 90=Sideways
        //TODO: cleanup fix cannonangle so it's not kooky
        float CannonAngle = transform.eulerAngles.z;
        if (CannonAngle >= 0 && CannonAngle <= 45){
            CannonAngle += 45;
        } else if (CannonAngle <= 360 && CannonAngle >= 315) {
            CannonAngle -= 315;
        }
        
        //reverse canonangle if outside limits
        if ((CannonAngle <= minAngle)) {
            transform.Rotate(0,0,1);
        } else if ((CannonAngle >= maxAngle)) {
            transform.Rotate(0,0,-1);
        }
        //Debug.Log(CannonAngle);

        //Shoot cannonball
        //if (Input.GetKeyDown(KeyCode.Space)){
        if ((T5Input.GetTrigger() >= .3)) {
            Debug.Log("Trigger press");

            if (canshoot) {
                StartCoroutine (FireCannon ()); 
                Debug.Log("Shot off!!");
            }

        }


        IEnumerator FireCannon() {
            canshoot = false;

            GameObject CreatedCannonBall = Instantiate(Cannonball,ShotPoint.position,ShotPoint.rotation);
            CreatedCannonBall.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * blastPower;

            yield return new WaitForSeconds (.2f);
            canshoot = true;
    
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T5Input = TiltFive.Input;

public class CannonBallController : MonoBehaviour
{
    public AudioClip enemyFalling;
    public AudioClip enemyDied;
    public AudioClip cannonballHitGround;
    public float volume=1f;
    private GameObject T5Wand;
    private GameObject T5Glasses;
    private Vector3 AudioPosition;

    void Start(){
        if (T5Input.GetWandAvailability()){
            T5Wand = GameObject.Find ("TiltFiveWand");
            T5Glasses = GameObject.FindWithTag ("T5Glasses"); 
        }
    }

    void OnCollisionEnter(Collision collision){


        // build list of objects bullet can be destroyed by
        // Instantiated prefabs have text appended to them so 
        //  comparing using first 5 chars only
        List<string> strings = new List<string>();
        strings.Add("Groun");
        strings.Add("Enemy"); 

        //set AudioPosition based on if the wand & glasses are being used
        if (T5Input.GetWandAvailability()){
            //Debug.Log(T5Glasses.transform.position.x +"-"+ T5Glasses.transform.position.y +"-"+ T5Glasses.transform.position.z);
            AudioPosition=T5Glasses.transform.position;
        } else {
            AudioPosition=collision.transform.position;
        }

        if (strings.Contains(collision.gameObject.name.Substring(0,5))){

            //controls what happens when enemy is hit
            if (collision.gameObject.name.Substring(0,5) == "Enemy"){
                //Only raycast for layer 7 (ground layer)
                LayerMask layerMask = 1 << 7;
                RaycastHit enemyDown;
                Ray downRay = new Ray(transform.position, -Vector3.up);

                //control enemy movement (not hits)
                //downray, the hit object, max distance, layer to allow ray to collide with
                if (Physics.Raycast(downRay, out enemyDown, 50f, layerMask) ){
                    if (enemyDown.distance >= 2){
                        ///enemy hit in air so remove all drag so he falls
                        collision.rigidbody.drag=0;
                        collision.rigidbody.mass=1000;
                        //don't let it get hit again
                        //collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                        //don't let parachutist get knocked away
                        collision.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        //remove parachute
                        collision.transform.Find("parachute").gameObject.SetActive(false);

                        AudioSource.PlayClipAtPoint(enemyFalling, AudioPosition, .1f);
                        Debug.Log("Shot Enemy Distance to ground supposed to be >= 2 but is actually:"+enemyDown.distance);
 
                    } else {
                        //enemy hit while on ground
                        AudioSource.PlayClipAtPoint(enemyDied, AudioPosition, volume);
                        Destroy(collision.gameObject);
                        Destroy(this.gameObject);
                        
                    }
                } 

            } else {
                //cannonball hit ground
                AudioSource.PlayClipAtPoint(cannonballHitGround, AudioPosition, .05f);
                Destroy(this.gameObject);
            }
            
        }
    }
}

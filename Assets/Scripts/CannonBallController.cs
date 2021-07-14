using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    public AudioClip enemyFalling;
    public AudioClip enemyHitGround;
    public AudioClip enemyDied;
    public float volume=1f;


    void OnCollisionEnter(Collision collision){


        // build list of objects bullet can be destroyed by
        // Instantiated prefabs have text appended to them so 
        //  comparing using first 5 chars only
        List<string> strings = new List<string>();
        strings.Add("Groun");
        strings.Add("Enemy");


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
                        ///enemy hit in air
                        collision.rigidbody.drag=0;
                        //don't let parachutist get knocked away
                        collision.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        //remove parachute
                        collision.transform.Find("parachute").gameObject.SetActive(false);

                        AudioSource.PlayClipAtPoint(enemyFalling, collision.transform.position, volume);
 
                    } else {
                        ///enemy hit on ground
                        AudioSource.PlayClipAtPoint(enemyHitGround, collision.transform.position, volume);
                        Destroy(collision.gameObject);
                        Destroy(this.gameObject);
                        
                    }
                } 

            } else {
                AudioSource.PlayClipAtPoint(enemyDied, collision.transform.position, volume);
                Destroy(this.gameObject);
            }
            
        }
    }
}

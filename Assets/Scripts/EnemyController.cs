using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using T5Input = TiltFive.Input;

public class EnemyController : MonoBehaviour
{
    
    public AudioClip enemyHitGround;
    public AudioClip enemyReachesCannon;
    public Transform killTarget;
    public int onGround = 0;
    private GameObject T5Wand;
    private GameObject T5Glasses;
    private Vector3 AudioPosition;
    
    private Rigidbody rb;

    void Start(){
        if (T5Input.GetWandAvailability()){
            T5Wand = GameObject.Find ("TiltFiveWand");
            T5Glasses = GameObject.FindWithTag ("T5Glasses"); 
        }
    }

    void FixedUpdate() {
        //Only raycast for layer 7 (ground layer)
        LayerMask layerMask = 1 << 7;
        RaycastHit enemyDown;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        
        //control enemy movement (not hits)
        //downray, the hit object, max distance, layer to allow ray to collide with
        if (Physics.Raycast(downRay, out enemyDown, 50f, layerMask)){
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Color.red);
            
            if (enemyDown.distance <= 1.5 && onGround == 0){

                //if the parachutist is falling without parachute then kill him when he hits the ground
                rb = gameObject.GetComponent<Rigidbody>();
                if (rb.drag == 0f){
                    //if enemy hits ground
                    if (T5Input.GetWandAvailability()){
                        AudioSource.PlayClipAtPoint(enemyHitGround, T5Glasses.transform.position, 1f);
                    }
                    Destroy(this.gameObject);
                }

                onGround = 1;
                gameObject.transform.Find("parachute").gameObject.SetActive(false);
                gameObject.AddComponent<NavMeshAgent>();

                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.destination = killTarget.position; 
                agent.speed = 1;

            }

        }
    }

    void OnCollisionEnter(Collision collision){

        //set AudioPosition based on if the wand & glasses are being used
        if (T5Input.GetWandAvailability()){
            //Debug.Log(T5Glasses.transform.position.x +"-"+ T5Glasses.transform.position.y +"-"+ T5Glasses.transform.position.z);
            AudioPosition=T5Glasses.transform.position;
        } else {
            AudioPosition=collision.transform.position;
        }

        Debug.Log("Enemy has collided with:" + collision.gameObject.name);

        if (collision.gameObject.name == "Cannon"){
            //if enemy hits cannon 
            GameObject gameController = GameObject.Find("GameController");
            GameController gc = gameController.GetComponent<GameController>();
            gc.CannonDamage += 1;
            AudioSource.PlayClipAtPoint(enemyReachesCannon, AudioPosition, 1f);
            Destroy(this.gameObject);    

        } else {

            //Debug.Log("Enemy has collided with unhandled object:" + collision.gameObject.name);
            //Destroy(this.gameObject);
        }
    }

}

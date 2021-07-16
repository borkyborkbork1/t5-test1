using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    public AudioClip enemyHitGround;
    public Transform killTarget;
    public int onGround = 0;
    
    private Rigidbody rb;

    void Start(){
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

                //Debug.Log("raycast hit distance  -- " + enemyDown.distance);
                //Debug.DrawLine(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Color.cyan);
                //Time.timeScale = 0;

                //if the parachutist is falling without parachute then kill him when he hits the ground
                rb = gameObject.GetComponent<Rigidbody>();
                if (rb.drag == 0f){
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
        // build list of objects enemy can be destroyed by
        List<string> strings = new List<string>();
        strings.Add("Cannon");

        if (strings.Contains(collision.gameObject.name)){

            GameObject gameController = GameObject.Find("GameController");
            GameController gc = gameController.GetComponent<GameController>();
            gc.CannonDamage += 1;


            Destroy(this.gameObject);
        }
    }

}

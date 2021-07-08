using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    public Transform killTarget;
    int onGround = 0;

    void FixedUpdate() {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        
        
        if (Physics.Raycast(downRay, out hit)){
            //Debug.Log("raycast hit!!!  -- " + hit.distance);
            
            if (hit.distance <= 1.5 && onGround == 0){
                onGround = 1;

                gameObject.AddComponent<NavMeshAgent>();

                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.destination = killTarget.position; 
                agent.speed = 1;

                /*
                float distanceToGround = transform.position.y - hit.distance;
                Debug.Log("Distance to ground: "+distanceToGround);
                Debug.Log("On ground");
                */
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

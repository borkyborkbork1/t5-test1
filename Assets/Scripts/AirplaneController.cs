using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    float speed = 5;
    public GameObject Enemy;
    private GameController gc;
    
    // Start is called before the first frame update
    void Start()
    {        
        //Grab gamecontroller gameobject
        GameObject GameController = GameObject.Find("GameController");
        gc = GameController.GetComponent<GameController>();

        //spawn enemy parachutists
        //-- (Pass in number, min & max seconds between)
        //spacing really controlls how many are spawned due to plane being destroyed when it gets offscreen
        StartCoroutine(SpawnEnemies(100, gc.enemyDropSpacingMin, gc.enemyDropSpacingMax));
    }

    // Update is called once per frame
    void Update()
    {
        
        //move plane
        if (gc.planeDirection == "vertical"){
            gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
            //kill off plane after it does it's pass
            if (gameObject.transform.position.z <= -40f){
                Destroy(gameObject);
            }
        } else {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            //kill off plane after it does it's pass
            if (gameObject.transform.position.x >= 40f){
                Destroy(gameObject);
            }
        }

        
    }

    //create enemy parachutists
    //-- (Pass in number, min & max seconds between)
    IEnumerator SpawnEnemies(int number, int minSpacing, int maxSpacing)
    {    
        for (int i = 0; i < number; i++){ 

            if(gameObject.transform.position.z >= -20){
                //space enemies randomly between passed min/max seconds
                float spacing = Random.Range(minSpacing, maxSpacing); 
                
                //yield on a new YieldInstruction that waits for spacing seconds.
                yield return new WaitForSeconds(spacing);
                Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
                //Debug.Log(spacing);
            }   
        }
    }
}


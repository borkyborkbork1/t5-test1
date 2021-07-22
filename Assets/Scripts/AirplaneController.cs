using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    float speed = 5;
    public GameObject Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        //spawn enemies. Pass in number, min & max seconds between
        //spacing really controlls how many are spawned due to plane being destroyed
        StartCoroutine(SpawnEnemies(100, 2, 6));
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);

        //kill off plane after it does it's pass
        if (gameObject.transform.position.z <= -40f){
            Destroy(gameObject);
        }
    }


    IEnumerator SpawnEnemies(int number, int minSpacing, int maxSpacing)
    {
        //create enemies      
        for (int i = 0; i < number; i++){ 

            if(gameObject.transform.position.z >= -20){
                //space enemies randomly between passed min/max seconds
                float spacing = Random.Range(minSpacing, maxSpacing); 
                
                //yield on a new YieldInstruction that waits for spacingg seconds.
                yield return new WaitForSeconds(spacing);
                Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
                //Debug.Log(spacing);
            }   
        }
    }
}


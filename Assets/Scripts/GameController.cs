using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject Enemy;
    public int CannonDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies(int number)
    {
        Debug.Log("Started enemy creation at timestamp : " + Time.time);

        for (int i = 0; i < number; i++){ 
            //create enemy
            Instantiate(Enemy, new Vector3(Random.Range(-50.0f, 50.0f), 20, 30), Quaternion.identity);

            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(5);
        }

        Debug.Log("Finished enemy creation at timestamp : " + Time.time);
    }
}

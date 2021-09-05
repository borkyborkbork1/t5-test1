using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int CannonDamage = 0;
    public GameObject airplane;
    Quaternion airplaneRotation = Quaternion.Euler(0, -90, 0);
    Vector3 airplaneSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //Start the creating planes
        //-- (number of planes, minimum, max for random seconds between them)    
        StartCoroutine(SpawnPlanes(50,10,30));

        //wait a bit then start spamming planes
        //new WaitForSeconds(90);
        //StartCoroutine(SpawnPlanes(50,5,10));
    }

    // Update is called once per frame
    void Update()
    {
        if (CannonDamage == 5){
            Debug.Log("*** Game Over ***");
            Time.timeScale = 0;
        }
    }

    //create enemy planes (number of planes, minimum & max for random seconds between them)     
    //-- enemy parachutists are controlled by AirplaneController
    IEnumerator SpawnPlanes(int number,int minSpacing,int maxSpacing){
        for (int i = 0; i < number; i++){ 
            //space planes randomly between passed min/max seconds
            float spacing = Random.Range(minSpacing, maxSpacing); 
            float planeXSpawn = Random.Range(-20, 20); 

            airplaneSpawn = new Vector3(planeXSpawn,20,38);
            Instantiate(airplane, airplaneSpawn, airplaneRotation);

            //yield on a new YieldInstruction that waits for spacing seconds.
            yield return new WaitForSeconds(spacing);

        }
    }

    
}

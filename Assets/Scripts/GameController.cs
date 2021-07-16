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
        airplaneSpawn = new Vector3(-15,20,38);
        StartCoroutine(SpawnPlanes(50,10,30));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CannonDamage == 50){
            Debug.Log("*** Game Over ***");
            Time.timeScale = 0;
        }


    }

IEnumerator SpawnPlanes(int number,int minSpacing,int maxSpacing){
        //create enemies      
        for (int i = 0; i < number; i++){ 
            //space planes randomly between passed min/max seconds
            float spacing = Random.Range(minSpacing, maxSpacing); 
            
            
            Instantiate(airplane, airplaneSpawn, airplaneRotation);

            //yield on a new YieldInstruction that waits for spacing seconds.
            yield return new WaitForSeconds(spacing);

        }
    }

    
}

using System.Collections;
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
        if (CannonDamage == 3){
            Debug.Log("*** Game Over ***");
            Time.timeScale = 0;
        }

        
    }






    IEnumerator SpawnEnemies(int number)
    {
        for (int i = 0; i < number; i++){ 
            //create enemy
            float EnemyZLocation = Random.Range(-30f, 30f);
            
            Instantiate(Enemy, new Vector3(10, 30, EnemyZLocation), Quaternion.identity);
            //Debug.Log(EnemyZLocation);

            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(5);
        }
    }
}

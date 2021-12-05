using System.Collections;
using UnityEngine;
using T5Input = TiltFive.Input;

public class GameController : MonoBehaviour
{

    public int cannonDamage = 0;
    public int planeWaves = 1;
    public int enemyDropSpacingMin = 5;
    public int enemyDropSpacingMax = 10;
    public string planeDirection = "vertical";
	public static bool isGamePaused = false;
	public GameObject pauseMenu;


    private bool spawnComplete = true;
    public GameObject airplane;
    Quaternion airplaneRotation = Quaternion.Euler(0, -90, 0);
    Vector3 airplaneSpawn;
    private GameObject T5Wand;
    private GameObject T5Glasses;


    // Start is called before the first frame update
    void Start()
    {
        Pause();

        Debug.Log("wand available:"+T5Input.GetWandAvailability());

		

        if (T5Input.GetWandAvailability()){
            T5Wand = GameObject.Find ("TiltFiveWand");
            T5Glasses = GameObject.FindWithTag ("T5Glasses"); 



        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isGamePaused) {
				Resume();
			} else {
				Pause();
			}
		}
		
		
		if (spawnComplete == true) {

            int numberOfPlanes = GameObject.FindObjectsOfType(typeof(AirplaneController)).Length;
            Debug.Log("number of planes left:"+numberOfPlanes);

            //dont start new wave till all planes are gone
            if (planeWaves == 1 && numberOfPlanes <= 0) {
                Debug.Log("Starting planewave: "+planeWaves);
                spawnComplete = false;
                enemyDropSpacingMin = 5;
                enemyDropSpacingMax = 10;
                //Start the creating planes
                //-- (number of planes, minimum, max for random seconds between them) 
                planeDirection = "vertical";
                airplaneRotation = Quaternion.Euler(0, -90, 0);//vertical
                StartCoroutine(SpawnPlanes(2,5,6));
            } else if (planeWaves == 2 && numberOfPlanes <= 0){

                Debug.Log("Starting planewave: "+planeWaves);
                spawnComplete = false;
                enemyDropSpacingMin = 2;
                enemyDropSpacingMax = 5;
                planeDirection = "horizontal";
                airplaneRotation = Quaternion.Euler(0, 180, 0);//horizontal
                StartCoroutine(SpawnPlanes(5,2,3));
               
            }
        }
        
        
        if (cannonDamage == 5){
            Debug.Log("*** Game Over ***");
            Time.timeScale = 0;
        }
    }

    //create enemy planes (number of planes, minimum & max for random seconds between them)     
    //-- enemy parachutists are controlled by AirplaneController
    IEnumerator SpawnPlanes(int number,int minSpacing,int maxSpacing){
        for (int i = 0; i < number; i++){ 

            Debug.Log("Plane Spawn/Number: " + i + "/" + number);

            //space planes randomly between passed min/max seconds
            float spacing = Random.Range(minSpacing, maxSpacing); 
            float randomSpawnLocation = Random.Range(-20, 20); 

            if (planeDirection == "vertical"){
                airplaneSpawn = new Vector3(randomSpawnLocation,20,38);
            } else {
                airplaneSpawn = new Vector3(-38,20,randomSpawnLocation);
            }

            GameObject plane = Instantiate(airplane, airplaneSpawn, airplaneRotation);

            //yield on a new YieldInstruction that waits for spacing seconds.
            yield return new WaitForSeconds(spacing);

            if (i >= number-1){
                //if this wave is complete set boolean and increment wave count
                spawnComplete = true;
                planeWaves = planeWaves + 1;
            }

        }

        
    }

    IEnumerator waitAFewSeconds () {
        yield return new WaitForSeconds (2f);
    }


	void Resume(){
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isGamePaused = false;
	}

	void Pause(){
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		isGamePaused = true;
	}


}

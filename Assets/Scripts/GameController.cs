using System.Collections;
using UnityEngine;
using T5Input = TiltFive.Input;
using MText;

public class GameController : MonoBehaviour
{

    public int cannonDamage = 0;
    public int planeWaves = 0;  //Displayed as level in the game
    public int score = 0;
    public int enemyDropSpacingMin = 5;
    public int enemyDropSpacingMax = 10;
    public string planeDirection = "vertical";
	public static bool isGamePaused = false;

	
	public GameObject wallLeft;
	public GameObject wallRight;
	public GameObject pauseMenu;
	public GameObject gameoverMenu;
	public Modular3DText ScoreUI;
	public Modular3DText CannonDamageUI;
	public Modular3DText LevelUI;


    private bool spawnComplete = true;
    public GameObject airplane;
    Quaternion airplaneRotation = Quaternion.Euler(0, -90, 0);
    Vector3 airplaneSpawn;
    private GameObject T5Wand;
    private GameObject T5Glasses;


    // Start is called before the first frame update
    void Start()
    {
        // show title screen on start
		Pause();

        if (T5Input.GetWandAvailability()){
            T5Wand = GameObject.Find ("TiltFiveWand");
            T5Glasses = GameObject.FindWithTag ("T5Glasses"); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // title and pause menu
		if (Input.GetKeyDown(KeyCode.Escape) || (T5Input.GetButtonUp(T5Input.WandButton.System)) ) {
			if (isGamePaused) {
				Resume();
			} else {
				Pause();
			}
		}

		if (T5Input.GetButtonUp(T5Input.WandButton.One)){
			//If game is paused and one button is hit then quit game
			if (isGamePaused) {
				Application.Quit();
			}
		}
		
		if (spawnComplete == true) {

            int numberOfPlanes = GameObject.FindObjectsOfType(typeof(AirplaneController)).Length;
            //Debug.Log("number of planes left:"+numberOfPlanes);

            //dont start new wave till all planes are gone
			if (numberOfPlanes <= 0) {
				planeWaves=planeWaves+1;
                //Debug.Log("Starting planewave: "+planeWaves);

				if (planeWaves >= 15){
					enemyDropSpacingMin = 1;
					enemyDropSpacingMax = 5;
				} else if (planeWaves >= 10) {
					enemyDropSpacingMin = 3;
					enemyDropSpacingMax = 6;
					//wallRight.SetActive(false);
					//wallLeft.SetActive(false);
				} else if (planeWaves >= 5) {
					enemyDropSpacingMin = 4;
					enemyDropSpacingMax = 6;
					//wallRight.SetActive(false);
					//wallLeft.SetActive(true);
				} else if (planeWaves >= 2) {
					enemyDropSpacingMin = 4;
					enemyDropSpacingMax = 8;
					//TODO: Figure out dynamic mesh calc
					//wallLeft.SetActive(false);
				}


				if (planeWaves % 2 == 1){
					planeDirection = "vertical";	
					airplaneRotation = Quaternion.Euler(0, -90, 0);//vertical
				} else {
					planeDirection = "horizontal";
					airplaneRotation = Quaternion.Euler(0, 180, 0);//horizontal
				}
				StartCoroutine(SpawnPlanes(planeWaves+1,enemyDropSpacingMin,enemyDropSpacingMax));
			}
        }
        //Update UI
		ScoreUI.Text = "Score: " + score.ToString();
		CannonDamageUI.Text = "Cannon Damage: " + cannonDamage.ToString();
		LevelUI.Text = "Level: " + planeWaves.ToString();


        if (cannonDamage == 100){
			// TODO: allow for restart from gameover screen
            Debug.Log("*** Game Over ***");
			PauseGameover();
        }
    }

    //create enemy planes (number of planes, minimum & max for random seconds between them)     
    //-- enemy parachutists are controlled by AirplaneController
    IEnumerator SpawnPlanes(int number,int minSpacing,int maxSpacing){
        for (int i = 0; i < number; i++){ 

           //Debug.Log("Plane Spawn/Number: " + i + "/" + number);

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

	void PauseGameover(){
		gameoverMenu.SetActive(true);
		Time.timeScale = 0f;
		isGamePaused = true;
	}
}

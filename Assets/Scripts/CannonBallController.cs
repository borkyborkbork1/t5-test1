using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision){


        // build list of objects bullet can be destroyed by
        // Instantiated prefabs have text appended to them so 
        //  comparing using first 5 chars only
        List<string> strings = new List<string>();
        strings.Add("Groun");
        strings.Add("Enemy");

        if (strings.Contains(collision.gameObject.name.Substring(0,5))){

            if (collision.gameObject.name.Substring(0,5) == "Enemy"){
                Destroy(collision.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}

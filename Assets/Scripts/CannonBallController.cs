using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision){
         
        // build list of objects bullet can be destroyed by
        List<string> strings = new List<string>();
        strings.Add("Ground");
        strings.Add("Enemy");

        if (strings.Contains(collision.gameObject.name)){
            if (collision.gameObject.name == "Enemy"){
                Destroy(collision.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}

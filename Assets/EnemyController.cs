using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
         
        // build list of objects to be destroyed by
        List<string> strings = new List<string>();
        strings.Add("CannonBall");

        if (strings.Contains(collision.gameObject.name)){
            Destroy(this.gameObject);
        }
    }
}

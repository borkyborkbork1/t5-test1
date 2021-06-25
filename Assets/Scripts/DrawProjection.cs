using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class DrawProjection : MonoBehaviour
{
    CannonController cannonController;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 100;

    // Distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    // Manually create a new layer for the Cannonball, and the below will be manually set to 
    // all layers but Cannonball layer
    public LayerMask CollidableLayers;

    void Start(){
        cannonController = GetComponent<CannonController>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //this projection code is derived from the tutorial here:
        //https://www.youtube.com/watch?v=RnEO3MRPr5Y

        //let linerenderer know how many points we plan on giving it
        lineRenderer.positionCount = numPoints;

        //create a list of type vector3's
        List<Vector3> points = new List<Vector3>();
 
        //get cannon shotpoint info from other script
        Vector3 startingPosition = cannonController.ShotPoint.position;
        Vector3 startingVelocity = cannonController.ShotPoint.up * cannonController.blastPower;

        //build array for rendering an arced line based on position, velocity & gravity
        for (float t = 0; t < numPoints; t += timeBetweenPoints){
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y/2f * t * t;
            points.Add(newPoint);

            //end building array if the new point collides with anything
            if (Physics.OverlapSphere(newPoint, 1, CollidableLayers).Length > 0){
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());

    }
}

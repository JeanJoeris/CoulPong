using UnityEngine;
using System.Collections;

public class BoundaryController : MonoBehaviour {

    public bool ForceVectorFlip; //used to flip force vector depending on top wall vs bot wall, and the wall charge

    public float ForceStrength;

    private GameObject ballObject;

    private Rigidbody rbBall;

    private float seperation;

    private float Force;

    void Start()
    {
        ballObject = GameObject.FindWithTag("Ball");
        rbBall = ballObject.GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        AddWallForceToBall(rbBall);
    }

    public void AddWallForceToBall(Rigidbody rbBall)
    {
        seperation = gameObject.transform.position.z - rbBall.position.z;
        Debug.Log(seperation);
        Force = -1 / Mathf.Pow(seperation, 2)*ForceStrength;

        if (ForceVectorFlip == true)
        {
            rbBall.AddForce(0, 0, -Force);
        }

        if (ForceVectorFlip != true)
        {
            rbBall.AddForce(0, 0, Force);
        }

        
    }

}

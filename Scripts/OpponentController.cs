using UnityEngine;
using System.Collections;

public class OpponentController : MonoBehaviour {

    public Vector3[] PredictedPosition;
    

    public float Speed;

    private int numTimeSteps;

    private Vector3 seperation;

    private Rigidbody rb;
    private Rigidbody rbBall;

	
	void Start ()
    {

        numTimeSteps = 1;
        PredictedPosition = new Vector3[numTimeSteps];

        rb = GetComponent <Rigidbody>();

        GameObject ball = GameObject.FindWithTag("Ball");
        rbBall = ball.GetComponent<Rigidbody>();
	
	}


    void FixedUpdate()
    {
        CalcPredictedPositon();
        MoveToPredictedPosition();
    }

    private void MoveToPredictedPosition()
    {
        float step = Speed * Time.deltaTime;
        Vector3 target = Vector3.ProjectOnPlane(PredictedPosition[numTimeSteps - 1], Vector3.left);
        rb.position = Vector3.MoveTowards(rb.position, new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(target.z, (-10 + 3f) / 2, (10 - 3f) / 2)), step);
    }

    private void CalcPredictedPositon()
    {
        PredictedPosition[0] = rbBall.position;
        seperation = rb.position - rbBall.position;

        for (int i = 1; i < numTimeSteps; i++)
        {
            PredictedPosition[i] = PredictedPosition[i - 1] + transform.TransformVector(rbBall.velocity) * Time.deltaTime * i
                + 1 / 2 * 1 / seperation.sqrMagnitude * seperation.normalized * Mathf.Pow(Time.deltaTime, 2) * i;
        }
    }
}

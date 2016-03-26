using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {


    public float Speed;
    public float ForceStrength;
    public float MaxSpeed;
    

    public Material PlayerMaterial;
    public Material OpponentMaterial;

    private int chargeSignPlayer;
    private int chargeSignOpponent;

    private float volleyWait;
    private float startAngle;

    private Vector3 separationPlayer;
    private Vector3 seperationOpponent;
    private Vector3 forcePlayer;
    private Vector3 forceOpponent;

    private GameObject playerObject;

    private Rigidbody rb;
    private Rigidbody rbPlayer;
    private Rigidbody rbOpponent;


    void Start()
    {
        chargeSignPlayer = 1;
        chargeSignOpponent = 1;
        volleyWait = 2f;

        rb = GetComponent<Rigidbody>();

        SetPlayerCharge();

        StartCoroutine(SpawnBall(0));
        

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            rbPlayer = playerObject.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Cannot find player");
        }

        GameObject opponentObject = GameObject.FindWithTag("Opponent");
        if (opponentObject != null)
        {
            rbOpponent = opponentObject.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Cannot find opponent");
        }

    }
	
    void Update()
    {
        SetPlayerCharge();
        SetOpponentCharge();
    }

    void FixedUpdate()
    {
        AddForces(rb, rbPlayer.position, rbOpponent.position);
        rb.velocity = ClampVelocityPosition(rb.velocity, MaxSpeed);
    }

    private void SetOpponentCharge()
    {
        if (rb.velocity.x > 0 & rb.position.x > 0) 
        {
            chargeSignOpponent = 1;
            OpponentMaterial.SetColor("_Color", Color.blue);
        }

        if (rb.velocity.x < 0)
        {
            chargeSignOpponent = -1;
            OpponentMaterial.SetColor("_Color", Color.red);
        }
    }

    private void SetPlayerCharge()
    {
        if (Input.GetButtonDown("Jump" ))
        {
            chargeSignPlayer *= -1;
        }

        if (chargeSignPlayer == 1)
        {
            PlayerMaterial.SetColor("_Color", Color.blue);
        }
        else
        {
            PlayerMaterial.SetColor("_Color", Color.red);
        }

        
    }

    private void AddForces(Rigidbody rbBall, Vector3 playerPosition, Vector3 opponentPosition)
    {
        separationPlayer = playerPosition - rb.position;
        seperationOpponent = opponentPosition - rb.position;

        forcePlayer = 1 / separationPlayer.sqrMagnitude * separationPlayer.normalized * ForceStrength * chargeSignPlayer;
        forcePlayer = 1 / seperationOpponent.sqrMagnitude * seperationOpponent.normalized * ForceStrength * chargeSignOpponent;
        rb.AddForce(forceOpponent);
        rb.AddForce(forcePlayer);
    }

    private Vector3 ClampVelocityPosition(Vector3 velocity, float maxSpeed) 
    {
        return new Vector3(Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(velocity.y, 0, 0),
                    Mathf.Clamp(velocity.z, -maxSpeed, maxSpeed));

    }

    IEnumerator SpawnBall(float volleyWait)  
    {
        yield return new WaitForSeconds(volleyWait);
        startAngle = Random.Range(-45,45) * Mathf.PI / 180;
        rb.position = Vector3.zero;
        rb.velocity = Speed * (Random.Range(0, 2) * 2 - 1) * new Vector3(Mathf.Cos(startAngle), 0, Mathf.Sin(startAngle));

    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            rb.velocity = new Vector3(-rb.velocity.x, 0, rb.velocity.z);
        }

        if (other.CompareTag("Goal"))
        {
            StartCoroutine(SpawnBall(volleyWait));
        }

        else
        {
            return;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, -rb.velocity.z);   
        }
    }
}

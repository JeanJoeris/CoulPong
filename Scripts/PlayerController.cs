using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {

    public float Speed;
    public float Width;
    public float PlayerWidth;


    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	    
	}
	

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");

        rb.position += new Vector3(0, 0, -MoveHorizontal) * Time.deltaTime * Speed;
        rb.position = new Vector3(rb.position.x, rb.position.y, 
            Mathf.Clamp(rb.position.z, (-Width + PlayerWidth) / 2, (Width - PlayerWidth) / 2));

    }

}

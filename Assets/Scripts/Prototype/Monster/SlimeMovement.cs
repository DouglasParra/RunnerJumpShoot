using UnityEngine;
using System.Collections;

public class SlimeMovement : MonoBehaviour {

    public float speedX;
    public float speedY;

    [Header("Ground Check")]
    [Space(10)]
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        myRigidbody.velocity = new Vector2(speedX, myRigidbody.velocity.y);

        if (grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, speedY);
        }

	}
}

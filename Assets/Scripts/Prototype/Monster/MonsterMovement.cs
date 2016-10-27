using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

    public float jumpForce;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            //Debug.Log(other.name + " - " + other.GetComponent<Rigidbody2D>().velocity);
        }
    }
}

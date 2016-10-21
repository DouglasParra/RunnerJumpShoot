using UnityEngine;
using System.Collections;

public class MegaManShotMovement : MonoBehaviour {

    public int damage;
    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed, 0, 0);
        Destroy(this.gameObject, 2);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            speed = 0;
            GetComponent<Animator>().SetBool("Hit", true);
        }
    }

    public void destroyShot()
    {
        Destroy(this.gameObject);
    }
}

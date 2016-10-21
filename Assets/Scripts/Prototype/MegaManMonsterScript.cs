using UnityEngine;
using System.Collections;

public class MegaManMonsterScript : MonoBehaviour {

    public int damage;
    public int hp;

    public GameObject platformDestructionPoint;

    // Use this for initialization
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    public void RemoveHP(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shot")
        {
            RemoveHP(other.GetComponent<MegaManShotMovement>().damage);
        }
    }
}

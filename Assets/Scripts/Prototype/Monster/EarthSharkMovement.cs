using UnityEngine;
using System.Collections;

public class EarthSharkMovement : MonoBehaviour {

    [Tooltip("Velocidade horizontal no movimento inicial")]
    public float speedX;

    [Tooltip("Velocidade vertical para o pulo")]
    public float speedY;

    [Tooltip("Tempo de espera antes de dar o bote")]
    public float waitTime;

    private float timeInitialMovement;
    private bool canInitialMovement;
    private bool canAttack;

    private GameObject monsterPoint;
    private GameObject player;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        monsterPoint = GameObject.Find("MonsterPoint");
        player = GameObject.FindWithTag("Player");
        //transform.parent = monsterPoint.transform.parent;

        //timeInitialMovement = Time.time;
        canInitialMovement = false;
        canAttack = false;

        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x - player.transform.position.x <= 20 && !canInitialMovement)
        {
            canInitialMovement = true;
            timeInitialMovement = Time.time;
            StartCoroutine(EarthSharkInitialMovement());
        }

        if (canAttack)
        {
            canAttack = false;
            myRigidbody.isKinematic = false;
            myRigidbody.velocity = new Vector2(-speedY, speedY);
        }
	}

    IEnumerator EarthSharkInitialMovement()
    {
        yield return new WaitForSeconds(.033f);

        if (transform.position.x > monsterPoint.transform.position.x)
        {
            transform.Translate(speedX, 0, 0);
        }
        else
        {
            transform.parent = monsterPoint.transform.parent;
        }

        if (Time.time >= timeInitialMovement + waitTime && transform.position.x < monsterPoint.transform.position.x)
        {
            transform.parent = null;
            canAttack = true;
            yield break;
        }

        yield return EarthSharkInitialMovement();
    }
}

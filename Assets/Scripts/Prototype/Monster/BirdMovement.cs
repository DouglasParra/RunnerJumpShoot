using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

    private GameObject monsterPoint;

    [Tooltip("Velocidade horizontal na descida")]
    public float speedX;

    [Tooltip("Velocidade vertical na descida")]
    public float speedY;

    [Tooltip("Tempo de espera antes do rasante")]
    public float waitTime;

    private float timeInitialMovement;
    private bool canInitialMovement;
    private bool canAttack;

    private GameObject player;

	// Use this for initialization
	void Start () {
        monsterPoint = GameObject.Find("MonsterPoint");
        player = GameObject.FindWithTag("Player");

        canInitialMovement = false;
        canAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x - player.transform.position.x <= 20 && !canInitialMovement)
        {
            canInitialMovement = true;
            timeInitialMovement = Time.time;
            transform.parent = monsterPoint.transform.parent;
            StartCoroutine(BirdInitialMovement());
        }

        if (canAttack)
        {
            transform.Translate(speedX, speedY, 0);
        }
	}

    IEnumerator BirdInitialMovement()
    {
        yield return new WaitForSeconds(.033f);

        if (transform.position.x > monsterPoint.transform.position.x)
        {
            transform.Translate(-0.1f, 0, 0);
        }

        if (transform.position.y > 5f)
        {
            transform.Translate(0, -.1f, 0);
        }

        if (Time.time >= timeInitialMovement + waitTime)
        {
            transform.Rotate(new Vector3(0, 0, 20));
            canAttack = true;
            yield break;
        }

        yield return BirdInitialMovement();
    }
}

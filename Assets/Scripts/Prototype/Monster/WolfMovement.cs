using UnityEngine;
using System.Collections;

public class WolfMovement : MonoBehaviour {

    [Tooltip("Velocidade horizontal no movimento inicial")]
    public float speedX;

    [Tooltip("Tempo de espera antes de dar o bote")]
    public float waitTime;

    public GameObject shotPrefab;

    private float timeInitialMovement;
    private bool canInitialMovement;
    private bool canAttack;

    private GameObject monsterPoint;
    private GameObject player;
    private Rigidbody2D myRigidbody;
    private GameObject shot;

    // Use this for initialization
    void Start()
    {
        monsterPoint = GameObject.Find("MonsterPoint");
        player = GameObject.FindWithTag("Player");

        canInitialMovement = false;
        canAttack = false;

        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x <= 20 && !canInitialMovement)
        {
            canInitialMovement = true;
            timeInitialMovement = Time.time;
            StartCoroutine(WolfInitialMovement());
        }

        if (canAttack)
        {
            if (!shot.Equals(null))
            {
                if (Time.time + waitTime >= timeInitialMovement)
                {
                    StartCoroutine(WolfFinalMovement());
                }
            }
        }
    }

    IEnumerator WolfInitialMovement()
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

            shot = (GameObject)Instantiate(shotPrefab, transform.position, Quaternion.identity);
            shot.SetActive(true);

            yield break;
        }

        yield return WolfInitialMovement();
    }

    IEnumerator WolfFinalMovement()
    {
        yield return new WaitForSeconds(.033f);

        if (transform.position.x < player.transform.position.x + 20)
        {
            transform.Translate(0.025f, 0, 0);
        }
        else if (transform.position.x > player.transform.position.x + 20)
        {
            Destroy(this.gameObject);
        }

        yield return WolfFinalMovement();
    }
}

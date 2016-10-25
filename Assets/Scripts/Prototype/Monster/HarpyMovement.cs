using UnityEngine;
using System.Collections;

public class HarpyMovement : MonoBehaviour {

    [Tooltip("Tempo de espera antes do rasante")]
    public float waitTime;

    public GameObject shotPrefab;

    private float timeInitialMovement;
    private bool canInitialMovement;
    private bool canAttack;

    private GameObject player;
    private GameObject monsterPoint;
    private GameObject shot;

    // Use this for initialization
    void Start()
    {
        monsterPoint = GameObject.Find("MonsterPoint");
        player = GameObject.FindWithTag("Player");

        canInitialMovement = false;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x <= 20 && !canInitialMovement)
        {
            canInitialMovement = true;
            timeInitialMovement = Time.time;
            transform.parent = monsterPoint.transform.parent;
            StartCoroutine(HarpyInitialMovement());
        }

        if (canAttack)
        {
            if (!shot.Equals(null))
            {
                transform.parent = null;
                if (Time.time + waitTime >= timeInitialMovement)
                {
                    StartCoroutine(HarpyFinalMovement());
                }
            }
        }

    }

    IEnumerator HarpyInitialMovement()
    {
        yield return new WaitForSeconds(.033f);

        if (transform.position.x > monsterPoint.transform.position.x)
        {
            transform.Translate(-0.1f, 0, 0);
        }

        if (transform.position.y > 3f)
        {
            transform.Translate(0, -.1f, 0);
        }

        if (Time.time >= timeInitialMovement + waitTime)
        {
            canAttack = true;

            shot = (GameObject)Instantiate(shotPrefab, transform.position, Quaternion.identity);
            shot.transform.Rotate(0, 0, -45);
            shot.SetActive(true);

            timeInitialMovement = Time.time;
            yield break;
        }

        yield return HarpyInitialMovement();
    }

    IEnumerator HarpyFinalMovement()
    {
        yield return new WaitForSeconds(.033f);

        if (transform.position.y < 10f)
        {
            transform.Translate(0, .0025f, 0);
        }

        yield return HarpyFinalMovement();
    }
}

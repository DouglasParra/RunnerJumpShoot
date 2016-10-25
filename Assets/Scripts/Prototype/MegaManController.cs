using UnityEngine;
using System.Collections;

public class MegaManController : MonoBehaviour {

    [Header("Movement")]
    public float runSpeed;
    public float jumpForce;

    public float speedMultiplier;
    public float speedIncreaseMilestone; 

    [Header("Ground Check")]
    [Space(10)]
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    [Header("Shot")]
    [Space(10)]
    public GameObject shotPosition;
    public GameObject shotLevel1;

    [Header("HP")]
    public int hp;

    private bool canDoubleJump;
    private float speedMilestoneCount;

    private GameManager gameManager;

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private GameObject playerPoint;

    private bool invincible;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(4).gameObject;

        speedMilestoneCount = speedIncreaseMilestone;
        invincible = false;
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            runSpeed = runSpeed * speedMultiplier;
        }

        if (grounded)
        {
            myAnimator.SetBool("Grounded", true);
        }
        else
        {
            myAnimator.SetBool("Grounded", false);
        }

        if (grounded)
        {
            canDoubleJump = true;
        }
        
        myRigidbody.velocity = new Vector2(runSpeed, myRigidbody.velocity.y);

        if (!grounded)
        {
            myAnimator.SetFloat("JumpVelocity", myRigidbody.velocity.y);
        }
	}

    public void Shoot()
    {
        myAnimator.SetTrigger("Shoot");
        GameObject shot = Instantiate(shotLevel1, shotPosition.transform.position, Quaternion.identity) as GameObject;
        shot.SetActive(true);
    }

    public void Jump()
    {

        if (grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        else if (!grounded && canDoubleJump)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            canDoubleJump = false;
        }
    }

    public void RemoveHP(int damage)
    {
        hp -= damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster" && !invincible)
        {
            invincible = true;
            RemoveHP(other.GetComponent<MegaManMonsterScript>().damage);
            gameManager.RemoveHP(hp);
            StartCoroutine(InvincibleFor3Seconds());
        }
        else if (other.gameObject.tag == "MonsterShot" && !invincible)
        {
            invincible = true;
            RemoveHP(other.GetComponent<MonsterShot>().damage);
            gameManager.RemoveHP(hp);
            StartCoroutine(InvincibleFor3Seconds());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster" && invincible)
        {
            invincible = false;
        }
    }

    IEnumerator InvincibleFor3Seconds()
    {
        yield return new WaitForSeconds(3f);
        invincible = false;
    }
}

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public MegaManController thePlayer;
    public GameObject playerPoint;

    private Vector3 lastPlayerPosition;
    private float distanceToMove;
    private float lastDistanceToMove;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<MegaManController>();
        lastPlayerPosition = thePlayer.transform.position;
        lastDistanceToMove = lastPlayerPosition.x;
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(thePlayer.gameObject.transform.position.x + " - " + playerPoint.transform.position.x);
        if (thePlayer.gameObject.transform.position.x < playerPoint.transform.position.x)
        {
            distanceToMove = 0;
        }
        else
        {
            distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
            //Debug.Log(distanceToMove);
        }

        StoppedPlayerVerification();

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
	}

    private void StoppedPlayerVerification()
    {
        // Se o jogador ficou parado
        //if (thePlayer.transform.position.x - lastDistanceToMove < 0)
        if (distanceToMove==0)
        {
            // Camera continua andando
            distanceToMove = .1f;
        }
        else
        {
            // Se não, atualiza lastDistanceToMove
            lastDistanceToMove = thePlayer.transform.position.x;
        }
    }
}

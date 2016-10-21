using UnityEngine;
using System.Collections;

public class EarthSharkMovement : MonoBehaviour {

    private float posX;
    private float posY;
    private float raio;

	// Use this for initialization
	void Start () {
        posX =  0.1f;

        StartCoroutine(EarthSharkInitialMovement());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator EarthSharkInitialMovement()
    {
        yield return new WaitForSeconds(.033f);

        posY = (Mathf.Sin(posX / 2)) - 4f;

        transform.position = new Vector3(posX, posY, transform.position.z);

        posX += .1f;

        if (posY > -3.22f)
        {
            StartCoroutine(EarthSharkMidMovement());
            yield break;
        }

        yield return EarthSharkInitialMovement();
    }

    IEnumerator EarthSharkMidMovement()
    {
        yield return new WaitForSeconds(.033f);

        transform.Translate(.1f, 0, 0);

        yield return EarthSharkMidMovement();
    }
}

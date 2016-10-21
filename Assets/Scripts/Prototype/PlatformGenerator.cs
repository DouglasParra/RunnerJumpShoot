using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    public Transform generationPoint;

    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public ObjectPooler[] theObjectPools;

    private float platformWidth;
    private float[] platformWidths;
    private int platformSelector;

    private MonsterGenerator monsterGenerator;

    private const float PLATFORM_WIDTH = 30.7f;

	// Use this for initialization
	void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            //platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
            platformWidths[i] = PLATFORM_WIDTH;
        }
	}

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            //transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 3) + distanceBetween, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

            //Instantiate(/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // Ativar coins
            ReactivateCoins(newPlatform);

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);          
        }
	}

    private void ReactivateCoins(GameObject newPlatform)
    {
        for (int i = 0; i < newPlatform.transform.childCount; i++)
        {
            if (newPlatform.transform.GetChild(i).tag.Equals("Coin"))
            {
                newPlatform.transform.GetChild(i).gameObject.SetActive(true);
            }
            else if(newPlatform.transform.GetChild(i).tag.Equals("Monster"))
            {
                Instantiate(newPlatform.GetComponent<MonsterGenerator>().monster, newPlatform.transform.GetChild(i).position, Quaternion.identity);
            }
        }
    }
}

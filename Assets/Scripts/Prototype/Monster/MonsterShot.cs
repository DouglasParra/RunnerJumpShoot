using UnityEngine;
using System.Collections;

public class MonsterShot : MonoBehaviour {

    [Tooltip("Velocidade horizontal do tiro")]
    public float shotSpeedX;

    [Tooltip("Velocidade vertical do tiro")]
    public float shotSpeedY;

    [Tooltip("Dano do tiro")]
    public int damage;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(shotSpeedX, shotSpeedY, 0);
	}

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip fireBomb;
    public AudioClip waterBomb;
    public AudioClip shockBomb;
    public AudioClip iceBomb;

    // Use this for initialization
    void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        if(gameObject.name == "EggElectricBomb(Clone)")
        {
            bombSound.clip = shockBomb;
        }
        else if (gameObject.name == "EggFireBomb(Clone)")
        {
            bombSound.clip = fireBomb;
        }
        else if (gameObject.name == "EggIceBomb(Clone)")
        {
            bombSound.clip = iceBomb;
        }
        else if (gameObject.name == "EggWaterBomb(Clone)")
        {
            bombSound.clip = waterBomb;
        }
        ExplodeAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		PlayAnim = true;
		Collider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
		//gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.fixedDeltaTime;
		if (Timer <= 0 && PlayAnim == true) {
            StartCoroutine(Explosion());
		}
	}

    private IEnumerator Explosion() {

        PlayAnim = false;
        ExplodeAnim.SetTrigger("Boom");
        Collider.enabled = true;
        bombSound.Play();
        GameObject.Find("Controller").GetComponent<ScreenShake>().BombGoesOff(0.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        if (Collider != null) {
            Collider.enabled = false;
        }
        Destroy(gameObject);
    }
}

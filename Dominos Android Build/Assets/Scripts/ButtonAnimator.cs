using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour {


	public bool Scale = true;
	Vector3 startScale;
	[Range(0,1)]
	public float unitToScale = 0.2f;
	public bool SclaleZoom = false;
	public float SpeedToScale = 1f;
	public bool FastUnScale = true;


	public bool Transparency = false;
	[Range(0,1)]
	public float targetTransparency = 0.5f;
	float startAlpha;
	Image image;


	public bool ChangeSprite = false;
	public Sprite pressedSprite, unPressedSprite;

	bool ButtonPressed = false;

	void Awake(){
		startScale = transform.localScale;
		if(Transparency || ChangeSprite){
			image = GetComponent<Image> ();
		}
		if(Transparency){
			startAlpha = image.color.a;
			}
		changeImageSprite ();
	}

	// Use this for initialization
	void Start () {
//		startScale = transform.localScale;	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (ButtonPressed) {
			if (Scale) {
				if (SclaleZoom) {
					transform.localScale = Vector3.MoveTowards (transform.localScale, 
						new Vector3 (startScale.x + unitToScale,
							startScale.y + unitToScale,
							startScale.z + unitToScale), Time.deltaTime * SpeedToScale);
				} else {
					transform.localScale = Vector3.MoveTowards (transform.localScale, 
						new Vector3 (startScale.x - unitToScale,
							startScale.y - unitToScale,
							startScale.z - unitToScale), Time.deltaTime * SpeedToScale);
				}
			}


			if(Transparency){

				image.color = new Color ( image.color.r,image.color.g,image.color.b,
					Mathf.MoveTowards(image.color.a , targetTransparency, Time.deltaTime * SpeedToScale));
			}



		} else {

			if (Scale) {
				if (FastUnScale) {
					transform.localScale = Vector3.MoveTowards (transform.localScale, startScale, 10f);
				} else {
					transform.localScale = Vector3.MoveTowards (transform.localScale, startScale, Time.deltaTime * SpeedToScale);
				}
			}


			if(Transparency){

				image.color = new Color ( image.color.r,image.color.g,image.color.b,
					Mathf.MoveTowards(image.color.a , startAlpha, Time.deltaTime * SpeedToScale));
			}

		}




	}


	void changeImageSprite(){
		if (ChangeSprite) {
			if (ButtonPressed) {
				image.sprite = pressedSprite;
			} else {
				image.sprite = unPressedSprite;
			}
		}
	}


	public void OnButtonDown(){
		ButtonPressed = true;
		changeImageSprite ();
	}
	public void OnButtonUp(){
		ButtonPressed = false;
		changeImageSprite ();
	}
}

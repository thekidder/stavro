using UnityEngine;
using System.Collections;

public class Stavro : MonoBehaviour {
	
	bool right = true;
	
	// Use this for initialization
	void Start () {
		this.GetComponent<OTAnimatingSprite>().frameIndex = 0;
		this.GetComponent<OTAnimatingSprite>().Pauze();
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw("Horizontal");
		
		if(x == 0f) 
		{
			if(right)
			{
				this.GetComponent<OTAnimatingSprite>().frameIndex = 0;
			}
			else
			{
				this.GetComponent<OTAnimatingSprite>().frameIndex = 3;
			}
			this.GetComponent<OTAnimatingSprite>().Pauze();
		}
		else if(x > 0f)
		{
			right = true;
			this.GetComponent<OTAnimatingSprite>().PlayLoop("walk_right");
		}
		else
		{
			right = false;
			this.GetComponent<OTAnimatingSprite>().PlayLoop("walk_left");
		}
		
		this.transform.position += new Vector3(x, 0f, 0f) * Time.deltaTime * 120f;
	}
}

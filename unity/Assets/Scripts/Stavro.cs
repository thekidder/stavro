using UnityEngine;
using System.Collections;

public class Stavro : MonoBehaviour {
    public GameObject land;

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

        this.transform.Translate(0f, -30f * Time.deltaTime, 0f);
        Shape landShape = land.GetComponent<ConvexShape>();
        Shape thisShape = this.GetComponent<ConvexShape>();
        Vector2 collision = CollisionDetector.MinTranslation(thisShape, landShape);
        if (collision != Vector2.zero) {
            this.transform.Translate(collision.x, collision.y, 0f);
        }
	}
}

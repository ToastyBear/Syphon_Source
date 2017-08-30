using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    private float lifetime = 5;

    private Material mat;
   // private Color originalCol;
    private float fadePercent;
    private float deathTime;
    private bool fading;

    
    // Use this for initialization
    void Start () {
        //mat = renderer.material;
        //mat = GetComponent<Material>();
        //originalCol = mat.color;
        //deathTime = Time.time + lifetime;

        //StartCoroutine("Fade");

	}
	
    IEnumerator Fade(){
        while (true) {
            yield return new WaitForSeconds(.2f);
            if (fading)
            {
                fadePercent += Time.deltaTime;
                //mat.color = Color.Lerp(originalCol, Color.clear, fadePercent);

                if (fadePercent >= 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (Time.time > deathTime)
                {
                    fading = true;
                }
            }
        }
   }
    void OnTriggerEnter(Collider coll) {
        //if (coll.tag == "Ground") {
        //    //gameObject.SetActive(false);
        //    // rigidbody.Sleep();
        //}
        if (coll.tag == "Wall") {
            gameObject.SetActive(false);
        }
        if (coll.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

    }
}

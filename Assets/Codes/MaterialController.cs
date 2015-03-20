using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class MaterialController : MonoBehaviour {

	public float ScaleReductionX = 1.0f;
	public float ScaleReductionY = 1.0f;
	private SpriteRenderer tmpSpriteRenderer;

	// Use this for initialization
	void Start () {
		tmpSpriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
		tmpSpriteRenderer.sharedMaterial.mainTextureScale = new Vector2( transform.localScale.x / ScaleReductionX ,transform.localScale.y / ScaleReductionY);
#else 
		tmpSpriteRenderer.Material.mainTextureScale = new Vector2( transform.localScale.x / ScaleReductionX ,transform.localScale.y / ScaleReductionY);
#endif

//		if (transform.localScale.x > transform.localScale.y)
//			tmpSpriteRenderer.material.mainTextureScale = new Vector2( transform.localScale.x / transform.localScale.y ,mainRow);
//		else
//			tmpSpriteRenderer.material.mainTextureScale = new Vector2(mainRow, transform.localScale.y / transform.localScale.x );
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		tmpSpriteRenderer = GetComponent<SpriteRenderer>();
		var tempMaterial = new Material(tmpSpriteRenderer.sharedMaterial);
		tempMaterial.mainTextureScale = new Vector2( transform.localScale.x / ScaleReductionX ,transform.localScale.y / ScaleReductionY);
		tmpSpriteRenderer.sharedMaterial = tempMaterial;
#endif
	}
	
}

using UnityEngine;

public class DNA : MonoBehaviour
{

	public float R; 
	public float G; 
	public float B;
	public float TimeToDie;
	
	private bool dead;
	private SpriteRenderer sRenderer;
	private Collider2D colllider;
	
	// Use this for initialization
	void Start ()
	{
		sRenderer = GetComponent<SpriteRenderer>();
		sRenderer.color = new Color(R, G, B);
		
		colllider = GetComponent<Collider2D>();
	}

	void OnMouseDown()
	{
		Die();
	}

	public void Die()
	{
		if(dead) return;
		
		dead = true;
		TimeToDie = PopulationManager.TimeElapsed;
		
		Debug.Log(string.Format("Died at {0}", TimeToDie));
		
		sRenderer.enabled = false;
		colllider.enabled = false;
	}
	
	public float Scale
	{
		get { return transform.localScale.x; }
		set
		{
			transform.localScale = new Vector3(value, value, value);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
	  int depth;//assumed zero, assigned 4 in inspector

	public float childScale;//assumed zero, assigned .5 in inspector
	public int maxDepth;
	public Mesh mesh;//A mesh contains at least a collection of points in 3D space plus a set of triangles – 
					//the most basic 2D shapes – defined by these points. 
					//The triangles constitute the surface of whatever the mesh represents.
	public Material material;//Materials consist of a shader and whatever data the shader needs. 
							 //Shaders are basically scripts that tell the graphics card how an object's polygons should be drawn.
	
	private  void Initialize (Fractal parent, Vector3 direction) {//this is like a copy constructor
		mesh = parent.mesh;//the mesh variable of a component of type Fractal
		material = parent.material;//the material variable of a component of type Fractal
		maxDepth = parent.maxDepth;//the same as the two above
		depth = parent.depth + 1;//when a new GameObject named FractalChild is made increment depth by 1
		transform.parent = parent.transform;
		childScale = parent.childScale;
		transform.localScale = Vector3.one * childScale;//the scale of the child is half the size of the parent
		transform.localPosition = direction * (0.5f + .5f * childScale);//the position of the child is moved in the specified direction 
	}

	private IEnumerator CreateChildren (){
		yield return new WaitForSeconds(1.0f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up);//with no restrictions, this 
																			//creates a loop in that at the start of 
																			//making a fractal, a new fractal component is called
		yield return new WaitForSeconds(1.0f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);

		yield return new WaitForSeconds(0.5f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.left);
	}

	private void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;//Returns the first instantiated Material assigned to the renderer.
		if (depth < maxDepth){
			/*new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up);
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);*/
			StartCoroutine(CreateChildren());
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("depth: " + depth);
	
	}
}

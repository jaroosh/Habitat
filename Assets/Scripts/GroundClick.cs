using UnityEngine;
using System.Collections;

public class GroundClick : MonoBehaviour {

	public GameObject objectPrefab;

	void OnMouseDown() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			SpawnSth(hit.point);
		}
	}

	void SpawnSth(Vector3 point) {
		GameObject go = (GameObject)Instantiate(objectPrefab);
		go.transform.position = point + new Vector3(0,0.01f, 0);
		
		var filter = go.GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();

		// setting the pivot manualy.
		float width = 1;
		float length = 5;

		Vector3[] vertices = {
			new Vector3(0,0,-width / 2),
			new Vector3(length,0,-width / 2),
			new Vector3(length,0,width/2),
			new Vector3(0,0,width/2)
		};
		
		Vector2[] uv = {
			new Vector2(0,0),
			new Vector2(length,0),
			new Vector2(length,1),
			new Vector2(0,1)
		};
		
		Vector3[] normals = {
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		
		int[] triangles = {
			2,1,0,
			0,3,2
		};
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.normals = normals;
		
		filter.mesh = mesh;

		// set up tiling.
		// niestety poniewaz tutaj wykorzystujemy juz wlasna kopie materialu - mamy dodatkowo jeden draw call
		// per object!!
		// prosty sposob zeby to przejsc to ATLASy czyli wybierasz poprostu czesc z tekstury i wtey
		// jedna tekstura jest wysylana - czyli 1 material, 2 teksutry.
	//	Vector2 texScale = new Vector2 (length, 1);
	//	var renderer = go.GetComponent<MeshRenderer> ().material.mainTextureScale = texScale;

	}
}

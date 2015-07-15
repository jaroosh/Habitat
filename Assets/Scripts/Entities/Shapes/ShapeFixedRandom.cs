using System;
using Habitat;
using UnityEngine;
using System.Collections;

public class ShapeFixedRandom : Shape {

	private int _tempInt;
	private float _meshGenerationTime;
	public int minBaseVertices = 3, maxBaseVertices = 5;

	/// <summary>
	/// Creates the geometry.
	/// </summary>
	protected void CreateGeometry(float radius0, float radius1, float thickness, float height, int sides, 
	                              int heightSegments, NormalsType normalsType, PivotPosition pivotPosition) {

		// Clear the old mesh and generate a new one.
		var meshFilter = GetComponent<MeshFilter>();
		if (meshFilter.sharedMesh == null)
		{
			meshFilter.sharedMesh = new Mesh();
		}		
		var mesh = meshFilter.sharedMesh;
		_meshGenerationTime = ShapesFactory.CreateDiamond(mesh, radius0, radius1, height, sides, heightSegments, normalsType, pivotPosition);

	}

//	protected override void Initialize() {
//		// setting the pivot manualy.
//		float width = 1;
//		float length = 5;
//
//		// Find random vertices.
//		_tempInt = UnityEngine.Random.Range (minBaseVertices, maxBaseVertices);
//		for(int i = 0; i < _tempInt; i++ ){
//
//		}
//
//
//		_vertices = new Vector3[] {
//			new Vector3(0,0,-width / 2),
//			new Vector3(length,0,-width / 2),
//			new Vector3(length,0,width/2),
//			new Vector3(0,0,width/2)
//		};
//		
//		_uvs = new Vector2[] {
//			new Vector2(0,0),
//			new Vector2(length,0),
//			new Vector2(length,1),
//			new Vector2(0,1)
//		};
//		
//		_normals = new Vector3[] {
//			Vector3.up,
//			Vector3.up,
//			Vector3.up,
//			Vector3.up
//		};
//		
//		_triangles = new int[]{
//			2,1,0,
//			0,3,2
//		};
////
//		
//		// set up tiling.
//		// niestety poniewaz tutaj wykorzystujemy juz wlasna kopie materialu - mamy dodatkowo jeden draw call
//		// per object!!
//		// prosty sposob zeby to przejsc to ATLASy czyli wybierasz poprostu czesc z tekstury i wtey
//		// jedna tekstura jest wysylana - czyli 1 material, 2 teksutry.
//		//	Vector2 texScale = new Vector2 (length, 1);
//		//	var renderer = go.GetComponent<MeshRenderer> ().material.mainTextureScale = texScale;
//	}
	
}

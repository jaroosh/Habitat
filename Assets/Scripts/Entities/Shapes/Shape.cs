using UnityEngine;
using System.Collections;
using PrimitivesPro;

/// <summary>
/// Represents an object "shape".
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public abstract class Shape : MonoBehaviour {
//
//	protected Vector3[] _vertices;
//	protected Vector3[] _normals;
//	protected Vector2[] _uvs;
//	protected int[] _triangles;
//	
//	public Vector3[] Vertices {
//		get { return _vertices;}
//	}
//	
//	public Vector3[] Normals {
//		get { return _normals; }
//	}
//
//	public Vector2[] UVs {
//		get { return _uvs;}
//	}
//
//	public int[] Triangles {
//		get { return _triangles;}
//	}
//
//	void Awake() {
//		Initialize ();
//		var filter = GetComponent<MeshFilter> ();		
//
//		if (filter.sharedMesh != null) {
//			return;
//		}
//
//		Mesh mesh = new Mesh ();			
//		mesh.vertices = Vertices;
//		mesh.triangles = Triangles;
//		mesh.uv = UVs;
//		mesh.normals = Normals;
//
//		mesh.RecalculateBounds();
//		//MeshUtils.CalculateTangents(mesh);
//
//		// Should we be using this here ? :0
//		mesh.Optimize();
//
//		filter.sharedMesh = mesh;	
//	}
//
//	protected abstract void Initialize();
}

using UnityEngine;
using System.Collections;

public class FactoryProvider : MonoBehaviour {

	public void CreateElement() {

		GameObject go = new GameObject ();
		var mf = go.AddComponent<MeshFilter> ();
		var mr = go.AddComponent<MeshRenderer> ();

		ShapesFactory.CreateDiamond (mf.sharedMesh, 1, 1, 10, 5, 5, Habitat.NormalsType.Vertex, Habitat.PivotPosition.Center);	
	}

}

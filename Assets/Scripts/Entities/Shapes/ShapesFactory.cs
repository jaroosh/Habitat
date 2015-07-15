using UnityEngine;
using System.Collections;
using Habitat;

/// <summary>
/// Factory of shapes.
/// </summary>
public class ShapesFactory : MonoBehaviour {

	/// <summary>
	/// Creates a diamond shape.
	/// </summary>
	/// <returns>The diamond.</returns>
	/// <param name="mesh">Mesh.</param>
	/// <param name="radius0">Radius0.</param>
	/// <param name="radius1">Radius1.</param>
	/// <param name="height">Height.</param>
	/// <param name="sides">Sides.</param>
	/// <param name="heightSegments">Height segments.</param>
	/// <param name="normalsType">Normals type.</param>
	/// <param name="pivotPosition">Pivot position.</param>
	public static float CreateDiamond(Mesh mesh, float radius0, float radius1, float height, int sides, int heightSegments, NormalsType normalsType, PivotPosition pivotPosition)
	{
		Debug.Log ("DIAMOND CREATED");
		return 0;
	}


}

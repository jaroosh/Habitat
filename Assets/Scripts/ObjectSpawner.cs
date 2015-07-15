using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Type of an entity.
/// </summary>
public enum  EntityType {
	Normal
}

public class ObjectSpawner : MonoBehaviour {

	private GameObject _tempObj;
	public GameObject[] entityPrefabs;

	private Dictionary<EntityType, GameObject> _entities;

	void Awake() {
		_entities = entityPrefabs.ToDictionary (k => k.GetComponent<Entity> ().type, k => k);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Spawn(hit.point, EntityType.Normal);
		}
	}

	/// <summary>
	/// Spawns an item of type.
	/// </summary>
	/// <param name="point">Point.</param>
	/// <param name="type">Type.</param>
	public void Spawn(Vector3 point, EntityType type) {
		// find the prefab.
		if(!_entities.ContainsKey (type)) {
			throw new ArgumentException("type");
		}

		GameObject go = (GameObject)Instantiate(_entities[type]);
		go.transform.position = point + new Vector3(0,0.01f, 0);
	}
}

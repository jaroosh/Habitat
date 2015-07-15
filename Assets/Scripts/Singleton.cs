using UnityEngine;
using System.Collections;

	/// <summary>
	/// Singleton class.
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected static T instance;
		
		/// <summary>
		/// One and only instance!
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if(instance == null)
				{
					instance = (T) FindObjectOfType(typeof(T));
					if (instance == null)
					{
						Debug.LogError("An instance of " + typeof(T) + 
						               " is needed in the scene, but there is none.");
					}
				}
				return instance;
			}
		}
	}
	

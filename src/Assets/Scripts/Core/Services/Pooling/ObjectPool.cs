using UnityEngine;
using System;
using System.Collections.Generic;

namespace Trixter.Core.Pooling {
	/// <summary>
	/// Object pool.
	/// </summary>
	/// <remarks>
	/// Original concept from http://forum.unity3d.com/threads/simple-reusable-object-pool-help-limit-your-instantiations.76851/.
	/// </remarks>
	public class ObjectPool {
		/// <summary>
		/// Member class for a prefab entered into the object pool
		/// </summary>
		[Serializable]
		public class ObjectPoolEntry {
			/// <summary>The prefab that will be pooled.</summary>
			public GameObject prefab;
			/// <summary>The quantity of objects to pre-instantiate.</summary>
			public int count;
			/// <summary>The objects currently pooled for the entry.</summary>
			public List<Transform> pool;
		}
		
		/// <summary>The objects that can be pooled.</summary>
		public List<ObjectPoolEntry> entries;
		/// <summary>The container that holds the pooled objects.</summary>
		protected Transform container;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Trixter.DevilRain.Core.Data.ObjectPool"/> class.
		/// </summary>
		public ObjectPool() {
			this.container = new GameObject("ObjectPool").GetComponent<Transform>();
		}

		/// <summary>
		/// Adds an entry to the pool.
		/// </summary>
		/// <param name="prefab">Prefab to be pooled.</param>
		/// <param name="count">Quantity of prefabs to pre-cache.</param>
		public void AddEntry(GameObject prefab, int count) {
			if (this.entries == null) {
				this.entries = new List<ObjectPoolEntry>(1);
			}

			this.entries.Add(new ObjectPoolEntry() {
				prefab = prefab,
				count = count
			});
		}

		/// <summary>
		/// Caches this pool items.
		/// </summary>
		public void Cache() {
			for (int entryIndex = 0; entryIndex < this.entries.Count; entryIndex++) {
				var objectPrefab = this.entries[entryIndex];

				objectPrefab.pool = new List<Transform>(objectPrefab.count);

				for (int n = 0; n < objectPrefab.count; n++) {
					var gameObject = this.InstantiatePrefab(objectPrefab.prefab);
					this.Despawn(gameObject);
				}
			}
		}

		/// <summary>
		/// Spawns a game object.
		/// 
		/// If no object of the type exists, returns false.
		/// </summary>
		/// <param name='prefabName'>The name of the prefab.</param>
		/// <returns>The instantiated prefab.</returns>
		public Transform Spawn(string prefabName) {
			Transform gameObject = null;

			for (int entryIndex = 0; entryIndex < this.entries.Count; entryIndex++) {
				var prefab = this.entries[entryIndex].prefab;
				
				if (prefab.name != prefabName) {
					continue;
				}
				
				if (this.entries[entryIndex].pool.Count > 0) {
					//Gets the first entry.
					gameObject = this.entries[entryIndex].pool[0];
					gameObject.transform.parent = null;
					gameObject.gameObject.SetActive(true);
					
					this.entries[entryIndex].pool.RemoveAt(0);
				} else {
					//Creates a new prefab.
					gameObject = this.InstantiatePrefab(prefab);
					this.entries[entryIndex].pool.Add(gameObject);
				}
			}

			if (gameObject != null) {
				gameObject.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
			}
			
			return gameObject;
		}
		
		/// <summary>
		/// Despawns a game object, pooling it.
		/// 
		/// Will not be pooled if there is no prefab of that type.
		/// </summary>
		/// <param name='gameObject'>Game object to be pooled.</param>
		public void Despawn(Transform gameObject) {
			for (int entryIndex = 0; entryIndex < entries.Count; entryIndex++) {
				if (this.entries[entryIndex].prefab.name != gameObject.name) {
					continue;
				}

				//If the game object is already pooled, exits.
				if (this.entries[entryIndex].pool.Contains(gameObject)) {
					break;
				}

				this.entries[entryIndex].pool.Add(gameObject);
				
				gameObject.BroadcastMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);
				
				gameObject.parent = this.container.transform;
				gameObject.gameObject.SetActive(false);
				if (gameObject.rigidbody != null) {
					gameObject.rigidbody.velocity = Vector3.zero;
				}

				break;
			}
		}

		/// <summary>
		/// Instantiates a new game object from a prefab.
		/// </summary>
		/// <param name="prefab">The game object prefab.</param>
		/// <returns>The game object.</returns>
		protected Transform InstantiatePrefab(GameObject prefab) {
			var gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab);
			gameObject.name = prefab.name;

			return gameObject.transform;
		}
	}
}
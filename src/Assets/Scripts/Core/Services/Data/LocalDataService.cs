using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Trixter.Core.Data {
	/// <summary>
	/// Handles files that are on disk.
	/// </summary>
	public class LocalDataService : IDataService {
		/// <summary>
		/// Saves a file.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <param name='data'>Data to be saved.</param>
		public void Save(string fileName, object data) {
			try {
				string path = this.GetPath(fileName);
				using (var file = File.Create(path)) {
					var formatter = new BinaryFormatter();
					formatter.Serialize(file, data);
				}
			} catch(Exception ex) {
				Debug.LogWarning("LocalDataManager.SaveFile(): Failed to serialize object to a file " + fileName + " (Reason: " + ex.ToString() + ")");
			}
		}
		
		/// <summary>
		/// Load a file.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <typeparam name='T'>Type of the file to be loaded.</typeparam>
		public T Load<T>(string fileName) {
			T data = default(T);
			
			try {
				string path = this.GetPath(fileName);
				if (File.Exists(path)) {				
					using (var file = File.Open(path, FileMode.Open)) {
						var formatter = new BinaryFormatter();
						data = (T)formatter.Deserialize(file);
					}
				}
			} catch(Exception ex) {
				Debug.LogWarning("LocalDataManager.LoadFile(): Failed to deserialize a file " + fileName + " (Reason: " + ex.ToString() + ")");
				data = default(T);
			}
			
			return data;
		}
		
		/// <summary>
		/// Checks if a file exists.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <returns>Boolean indicating if the file exists.</returns>
		public bool Exists(string fileName) {
			return File.Exists(this.GetPath(fileName));
		}
		
		/// <summary>
		/// Gets the path of a file.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <returns>The path.</returns>
		private string GetPath(string fileName) {
			return Path.Combine(Application.persistentDataPath, fileName);
		}
	}
}
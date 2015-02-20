using System;

namespace Trixter.Core.Data {
	/// <summary>
	/// Represents a data service to save and load files.
	/// </summary>
	public interface IDataService {
		/// <summary>
		/// Saves a file.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <param name='data'>Data to be saved.</param>
		void Save(string fileName, object data);
		
		/// <summary>
		/// Load a file.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <typeparam name='T'>Type of the file to be loaded.</typeparam>
		T Load<T>(string fileName);
		
		/// <summary>
		/// Checks if a file exists.
		/// </summary>
		/// <param name='fileName'>File name.</param>
		/// <returns>Boolean indicating if the file exists.</returns>
		bool Exists(string fileName);
	}
}
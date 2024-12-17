using System.Text.Json;

namespace PersonalBlog.DataAccess
{
	public class JsonDataAccess
	{
		private readonly string _directoryPath;

		public JsonDataAccess()
		{
			_directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
			if (!Directory.Exists(_directoryPath))
			{
				Directory.CreateDirectory(_directoryPath);
			}
		}

		public async Task WriteToJsonFileAsync<T>(string fileName, T data)
		{
			string filePath = Path.Combine(_directoryPath, fileName);
			string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
			await File.WriteAllTextAsync(filePath, jsonString);
		}
		public async Task<T> ReadFromJsonFileAsync<T>(string fileName)
		{
			string filePath = Path.Combine(_directoryPath, fileName);
			if (!File.Exists(filePath))
			{
				await File.WriteAllTextAsync(filePath, "[]");
			}
			string jsonString = await File.ReadAllTextAsync(filePath);
			return JsonSerializer.Deserialize<T>(jsonString);
		}
		public async Task<List<T>> ReadAllFromJsonFileAsync<T>(string fileName)
		{
			string filePath = Path.Combine(_directoryPath, fileName);
			if (!File.Exists(filePath))
			{
				await File.WriteAllTextAsync(filePath, "[]");
			}
			string jsonString = await File.ReadAllTextAsync(filePath);
			return JsonSerializer.Deserialize<List<T>>(jsonString);
		}

	}
}

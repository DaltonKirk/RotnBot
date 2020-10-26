using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RotnBot.Services
{
    public abstract class JsonFileLoader
    {
        // Loads Json text from file and return it deserialized into given type.
        public List<T> LoadJsonFromDisk<T>(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.WriteAllText(filename, "");
                }
                string json = System.IO.File.ReadAllText(filename);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occured when loading file form disk {ex.Message}");
                return new List<T>();
            }
        }

        // Saves an object as Json text to disk
        public void SaveJsonToDisk(string filename, object data)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(data));
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RotnBot.Services
{
    public abstract class JsonFileLoader
    {
        /// <summary>
        /// Loads JSON text from file and returns it deserialized into given type.
        /// </summary>
        /// <param name="filename"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> LoadJsonFromDisk<T>(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    return new List<T>();
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

        /// <summary>
        /// Serialize object into JSON text and saves to file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public void SaveJsonToDisk(string filename, object data)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(data));
        }
    }
}
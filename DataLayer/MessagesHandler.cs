using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DataLayer
{
    public class MessagesHandler
    {
        public List<string> Load(string filename)
        {
            List<string> data = new List<string>();
            string path = "./Data/" + filename; // Opens file in folder, Data
            try
            {
                using (var r = new StreamReader(path))
                {
                    while (!r.EndOfStream)
                    {
                        try
                        {
                            var line = r.ReadLine();
                            string[] values = line.Split(','); // Removes comma and appends to array
                            foreach (var value in values)
                            {
                                data.Add(value); // Adds word to list
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }// StreamReader is closed and flushed even with exception
            }
            catch(Exception)
            {
                return data;
            }
            return data;
        }

        public  bool Save(ArrayList data)
        {
            try
            {
                string filepath = "./Data/output.json";

                // Reference: https://www.youtube.com/watch?v=Ib3jnD158NI 
                JsonSerializer js = new JsonSerializer();
                if (File.Exists(filepath)) // Checks if file exists, if so, deletees it
                    File.Delete(filepath);

                // Used for persistance
                StreamWriter sw = new StreamWriter(filepath);
                JsonWriter jw = new JsonTextWriter(sw);

                foreach (object o in data)
                {
                    js.Serialize(jw, o); // Serialises each object
                }

                // Closes both writers
                jw.Close();
                sw.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

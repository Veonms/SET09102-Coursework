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
            string path = "./Data/" + filename;
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
                                data.Add(value);
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
                string filepath = "./Data/output.txt";

                // Reference: https://www.youtube.com/watch?v=Ib3jnD158NI 
                JsonSerializer js = new JsonSerializer();
                if (File.Exists(filepath))
                    File.Delete(filepath);

                StreamWriter sw = new StreamWriter(filepath);
                JsonWriter jw = new JsonTextWriter(sw);

                foreach (object o in data)
                {
                    js.Serialize(jw, o);
                }

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

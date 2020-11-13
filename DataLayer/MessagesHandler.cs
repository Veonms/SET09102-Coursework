using System;
using System.Collections.Generic;
using System.IO;

namespace DataLayer
{
    class MessagesHandler
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
                //Display error
                return data;
            }
            return data;
        }

        public void Save(List<string> data)
        {
            // Opens file using StreamWriter
            File.Create("output");
            using (var w = new StreamWriter(@"output"))
            {
                //string line = Staff.ReturnStaff();
                string line = "";
                w.WriteLine(line);
            } // StreamWriter is closed and flushed even with an exception
        }
        
    }
}

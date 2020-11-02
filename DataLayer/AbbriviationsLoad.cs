using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataLayer
{
    class AbbriviationsLoad
    {
        public void Load()
        {
            string filePath = System.IO.Path.GetFullPath("textwords.csv");
            using (var r = new StreamReader(filePath))
            {
                while (!r.EndOfStream)
                {
                    try
                    {
                        var line = r.ReadLine();
                        string[] values = line.Split(','); // Removes comma and appends to array
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }
            }// StreamReader is closed and flushed even with exception
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataLayer
{
    class MessagesHandler
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
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }// StreamReader is closed and flushed even with exception
        }

        public void Add()
        {
            // Opens file using StreamWriter
            using (var w = new StreamWriter(@"Output\Messages.csv"))
            {
                //string line = Staff.ReturnStaff();
                string line = "";
                w.WriteLine(line);
            } // StreamWriter is closed and flushed even with an exception
        }
        
    }
}

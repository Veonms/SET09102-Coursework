using System;
using System.IO;

namespace DataLayer
{
    class MessagesHandler
    {
        public Boolean Load(string filename)
        {
            string path = "./Data/" + filename;
            try
            {
                using (var r = new StreamReader(@path))
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
            catch(Exception)
            {
                //Display error
                return false;
            }
            return true;
        }

        public void Add()
        {
            // Opens file using StreamWriter
            using (var w = new StreamWriter(@"./Data\Messages.csv"))
            {
                //string line = Staff.ReturnStaff();
                string line = "";
                w.WriteLine(line);
            } // StreamWriter is closed and flushed even with an exception
        }
        
    }
}

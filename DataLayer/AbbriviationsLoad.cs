using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataLayer
{
    class AbbriviationsLoad
    {
        public Dictionary<string, string> Load()
        {
            string filePath = "./Data/textwords.csv";
            Dictionary<string, string> textwords = new Dictionary<string, string>();
            using (var r = new StreamReader(filePath))
            {
                while (!r.EndOfStream)
                {
                    try
                    {
                        var line = r.ReadLine();
                        string[] values = line.Split(','); // Removes comma and appends to array
                        if (values.Length > 2)
                        {
                            string temp = "";
                            for (int i=1; i<values.Length; i++)
                            {
                                temp += values[i] + " ";
                            }
                            textwords.Add(values[0],temp);
                        }
                        else
                        {
                            textwords.Add(values[0],values[1]);
                        }

                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }
            }// StreamReader is closed and flushed even with exception
            return textwords;
        }
    }
}

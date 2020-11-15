using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class FacadeSingleton
    {
        private static FacadeSingleton reference;

        private FacadeSingleton() { }

        public static FacadeSingleton GetInstance()
        {
            if (reference == null)
                reference = new FacadeSingleton();
            return reference;
        }
        
        // Used for persistance
        private AbbriviationsLoad al = new AbbriviationsLoad();
        private MessagesHandler mh = new MessagesHandler();

        public List<string> LoadMessage(string filename)
        {
            List<string> data = mh.Load(filename);
            return data;
        }
        public bool SaveMessages(ArrayList data)
        {
            try
            {
                ArrayList exportData = data;
                mh.Save(exportData);
                return true;
            }
            catch (Exception e)
            {
                var error = e;
                return false;
            }
        }
        public Dictionary<string, string> GetAbbreviations()
        {

            Dictionary<string, string> abb = al.Load();

            return abb;
        }


    }
}

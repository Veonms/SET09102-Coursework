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

        public static FacadeSingleton GetInstance() // Checks if Singleton exists
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
            List<string> data = mh.Load(filename); //Calls Load from MessagesHandler and stores list
            return data; // returns list
        }
        public bool SaveMessages(ArrayList data)
        {
            try
            {
                ArrayList exportData = data;
                mh.Save(exportData); // Calls Save from MessagesHandler
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Dictionary<string, string> GetAbbreviations()
        {

            Dictionary<string, string> abb = al.Load();//Calls Load from Abbriviations and stores dictionary

            return abb; // returns dictionary
        }


    }
}

using System;
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

        public Boolean Load(string filename)
        {
            Boolean success = mh.Load(filename);
            return success;
            //al.Load();
        }
        public void Save()
        {
            mh.Add();
        }


    }
}

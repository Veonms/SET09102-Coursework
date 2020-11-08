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
        private MessagesHandler ml = new MessagesHandler();

        public void Load()
        {
            ml.Load();
            al.Load();
        }
        public void Save()
        {
            ml.Add();
        }


    }
}

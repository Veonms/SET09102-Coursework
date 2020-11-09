using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class MessageFacade
    {
        public Boolean SaveMessages()
        {
            try
            {
                if (true)
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                return true;
            }
        }

        public Boolean Load(string filename)
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            Boolean success = fs.Load(filename);
            return success;
        }

        public void Save()
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            fs.Save();
            

        }
    }
}

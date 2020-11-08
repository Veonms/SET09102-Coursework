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

        public void Load()
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            fs.Load();
        }

        public void Save()
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            fs.Save();
            

        }
    }
}

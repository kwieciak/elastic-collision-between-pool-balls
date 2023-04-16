using System;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateAPIInstance()
        {
            return new DataAPI();
        }
    }
}

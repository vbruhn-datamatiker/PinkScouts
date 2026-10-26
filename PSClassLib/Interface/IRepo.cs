using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Interface
{
    public interface IRepo<T> where T : IHasId
    {


        T Create(T item);
        T Remove(int id);
        T Update(T item, int id);
        List<T> ReadAll();
        T GetById(int id);


    }
}

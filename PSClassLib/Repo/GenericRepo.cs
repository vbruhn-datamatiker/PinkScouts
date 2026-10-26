using PSClassLib.Interface;
using PSClassLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Repo
{
    public class GenericRepo<T> : IRepo<T>, IEnumerable<T> where T : IHasId
    {
        #region instance fields

        private List<T> _items;

        #endregion

        #region Properties

        public List<T> Items { get; set; }
  
        #endregion

        #region Constructors

        public GenericRepo()
        {
            _items = new List<T>();
        }

        public GenericRepo(List<T> items)
        {
            _items = items;
        }



        #endregion

        #region methods

        public List<T> ReadAll()
        {     
            return new List<T>(_items);
        }

        public T Create(T item)
        {
            _items.Add(item);
            return item;
        }

        public T Update(T newItem, int id)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            T itemFound = GetById(id);

            itemFound = newItem;

            return itemFound;

        }



        public T GetById(int id)
        {

            // using Find (implements a linear search in List<T>):

            T? item = _items.Find(item => item.Id == id);

            // Using for loop:

            //for (int i = 0; i < _items.Count; i++)
            //{
            //    if (_items[i].Id == id)
            //    {
            //        return _items[i];
            //    }
            //}

            if (item == null)
            {
                throw new ArgumentException("Does not exist");
            }


            return item;
        }

        public T Remove(int id)
        {

            T itemToRemove = GetById(id);

            _items.Remove(itemToRemove);

            return itemToRemove;
        }


        // Implements IEnumerator to use LINQ in services (?)
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion

    }
}

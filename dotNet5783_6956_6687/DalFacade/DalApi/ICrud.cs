using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface  ICrud<T>
    {
        T Get(int id);
        void Update(T entity);
        void Delete(int id);
        int Add(T other);
        IEnumerable<T> GetAll();

    }
}

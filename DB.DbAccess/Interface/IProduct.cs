using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbAccess.Interface
{
    public interface IProduct
    {
        void Insert(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> GetList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbAccess.Interface
{
    public interface IOrderDetail
    {
        void InsertOrderDetail(Product product);
        List<Product> GetList();
    }
}

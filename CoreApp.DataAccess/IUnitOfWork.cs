using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.DataAccess
{
    interface IUnitOfWork
    {
        bool SaveChanges();
    }
}

﻿using Core.EntityFrmework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramewrok.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfCategoryDal: EfEntityRepositoryBase<Category,MyContext>,ICategoryDal
    {
    }
}

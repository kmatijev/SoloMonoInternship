using System;
using System.Collections.Generic;
using Model.Grade;
using Repository.Common;
using Service.Common;

namespace DService
{
    public class Service: IService
    {
        List<Grade> GetGrade(int id)
        {
            return Repository.GetGrade(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.BusinessObjects
{
    public class BOUserId
    {
        public List<int> ExistUserIdList { get; set; } = new List<int>();
        public List<int> NotExistEmployeeCodeList { get; set; } = new List<int>();
    }
}

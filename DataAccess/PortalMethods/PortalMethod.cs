using DataAccess.BusinessObjects;
using DataAccess.PortalModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.PortalMethods
{
    public class PortalMethod
    {
        private static PortalMethod instance;
        private readonly PORTALOFFICE2011Context context = null;

        private PortalMethod ()
        {
            context = new PORTALOFFICE2011Context();
        }

        public static PortalMethod GetInstance()
        {
            if(instance == null)
            {
                instance = new PortalMethod();
            }
            return instance;
        }

        public int CheckLogin(string userName, string passWord)
        {
            
            string passwordHash = Code.Encrypt.EncryptPass("abcde", passWord);
            var returnValue = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.ReturnValue
            };

            var command = context.Database.GetDbConnection().CreateCommand();

            //Declare Procedure Name
            command.CommandText = "dbo.Core_MultiBlock_CheckLogin";

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(returnValue);
            command.Parameters.Add(new SqlParameter("@UserName", userName));
            command.Parameters.Add(new SqlParameter("@PasswordHash", passwordHash));

            if (context.Database.GetDbConnection().State == System.Data.ConnectionState.Closed)
                context.Database.GetDbConnection().Open();

            var result = command.ExecuteScalar();
            context.Database.GetDbConnection().Close();
            return (int)returnValue.Value;
            //var result = context.CoreUsers.FromSqlRaw("EXEC [dbo].[Core_MultiBlock_CheckLogin] @UserName, @PasswordHash, @ReturnValue OUTPUT", new SqlParameter("@UserName", userName).SqlDbType = System.Data.SqlDbType.NVarChar, new SqlParameter("@PasswordHash", passwordHash).SqlDbType = System.Data.SqlDbType.NVarChar, returnValue).ToList();
    
        }

        public BOUserId CheckExistUser(string[] employeeCodes)
        {

            BOUserId bOUserId = new BOUserId();
            //context.CoreUsers.Where(o =>
            //{
            //    string employeeCodeDB = Regex.Match(o.UserName, @"\d+").Value;
            //    if (employeeCodes.Contains(employeeCodeDB))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        notExistUserList += 
            //    }
            //});
            
            foreach (var item in employeeCodes)
            {
                var user = context.CoreUsers.FirstOrDefault(o => o.UserName.Substring(o.UserName.Length - 5, 5) == item);
                if(user == null)
                {
                    bOUserId.NotExistEmployeeCodeList.Add(int.Parse(item));
                }
                else
                {
                    bOUserId.ExistUserIdList.Add(user.UserId);
                }
            }
            return bOUserId;
        }

        public int Delete_UserGroups(List<int> userIds)
        {
            int count = 0;
            userIds.ForEach(o =>
            {

                var result = context.Database.ExecuteSqlInterpolated($"Execute Core_DeleteUserGroups {o}");
                if (result > 0)
                    count++;
            });
            return count;
        }

        public int Set_BlockGroupByUserId(List<int> listUserId)
        {
            int count = 0;
            listUserId.ForEach(userId => {

                var result = context.Database.ExecuteSqlInterpolated($"Execute Core_SetBlockGroup {userId}");
                if (result > 0)
                    count++;
            });
            return count;
        }

        public int Update_EmployeeUserPortalDept(List<string> listEmployeeCode)
        {
            int portalIdOffUser = 115;
            int DepartmentIdOffUser = 739;
            int count = 0;
            listEmployeeCode.ForEach(empCode => {

                var result = context.Database.ExecuteSqlInterpolated($"Execute HRM_UpdateEmployeeUserPortalDept {empCode}, {portalIdOffUser}, {DepartmentIdOffUser}");
                if (result > 0)
                    count++;
            });
            return count;
        }

        public int Update_EmployeeStatus(List<string> listEmployeeCode)
        {
            int status = 0;
            int count = 0;
            listEmployeeCode.ForEach(empCode =>
            {
                var empl = context.HrmEmployees.FirstOrDefault(o => o.EmployeeCode == empCode);
                if(empl != null)
                {
                    var result = context.Database.ExecuteSqlInterpolated($"Exec HRM_ChangeEmployeeStatus {empl.EmployeeId}, {status}");
                    if (result > 0)
                        count++;
                }
            });
            return count;
        }

        public int Update_UserPortalGroup(List<int> userIds)
        {
            int portalIdOffUser = 111;
            int groupIdOff = 900;
            int count = 0;
            userIds.ForEach(userId =>
            {
                var result = context.Database.ExecuteSqlInterpolated($"Execute Core_UpdateUserPortalGroup {userId}, {portalIdOffUser}, {groupIdOff}");
                if (result > 0)
                    count++;
            });
            return count;
        }
        public async Task CloseConnection()
        {
            await context.DisposeAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TestDAO: ShsTrainingContext
    {
        public int AddTest(Employee_info user)
        {
            try
            {
                db.Employee_info.Add(user);
                db.SaveChanges();
                return user.Id;
            }
            catch (DbEntityValidationException ex)
            {
                var firstError = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                    .FirstOrDefault();

                throw new Exception("Validation Error: " + firstError);
            }

        }


    }
}

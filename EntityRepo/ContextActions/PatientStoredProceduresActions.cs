using System;
using System.Data;
using System.Threading.Tasks;
using EntityRepo.ContextInterfaces;
using EntityRepo.CovidAppModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityRepo.ContextActions
{
    public class PatientStoredProceduresActions : CovidAppContext, IPatientStoredProcedureActions
    {
        public async Task<string> sp_InsertNewPatientDetailsAndAddress(string FirstName, string LastName, int Age,
                                                                       string Street, string Suburb, string State,
                                                                       string HospitalName, DateTime DateOfTest, string HealthCarerName,
                                                                       Boolean Result, string Notes)
        {
            using (var context = new CovidAppContext())
            {
                var result = "";
                await Task.Run(() =>
                {
                    using (var cmd = context.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "dbo.sp_InsertNewPatientDetailsAndAddress";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@firstName", SqlDbType.VarChar) { Value = FirstName });
                        cmd.Parameters.Add(new SqlParameter("@lastName", SqlDbType.VarChar) { Value = LastName });
                        cmd.Parameters.Add(new SqlParameter("@age", SqlDbType.Int) { Value = Age });
                        cmd.Parameters.Add(new SqlParameter("@street", SqlDbType.VarChar) { Value = Street });
                        cmd.Parameters.Add(new SqlParameter("@suburb", SqlDbType.VarChar) { Value = Suburb });
                        cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar) { Value = State });

                        cmd.Parameters.Add(new SqlParameter("@hospitalName", SqlDbType.VarChar) { Value = HospitalName });
                        cmd.Parameters.Add(new SqlParameter("@dateOfTest", SqlDbType.DateTime) { Value = DateOfTest });
                        cmd.Parameters.Add(new SqlParameter("@healthCarerName", SqlDbType.VarChar) { Value = HealthCarerName });
                        cmd.Parameters.Add(new SqlParameter("@results", SqlDbType.Bit) { Value = Result });
                        cmd.Parameters.Add(new SqlParameter("@notes", SqlDbType.VarChar) { Value = Notes });

                        if (cmd.Connection.State != ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }

                        result = cmd.ExecuteScalar().ToString();

                    };
                });

                return result;
            }

        }

        public async Task<string> sp_UpdatePatientAddress(int id, string Street, string Suburb, string State)
        {
            using (var context = new CovidAppContext())
            {
                var result = "";
                await Task.Run(() =>
                {

                    using (var cmd = context.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "dbo.sp_UpdatePatientAddress";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                        cmd.Parameters.Add(new SqlParameter("@street", SqlDbType.VarChar) { Value = Street });
                        cmd.Parameters.Add(new SqlParameter("@suburb", SqlDbType.VarChar) { Value = Suburb });
                        cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar) { Value = State });

                        if (cmd.Connection.State != ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }

                        result = cmd.ExecuteScalar().ToString();
                    };
                });

                return result;
            }
        }
        public async Task<string> sp_UpdatePatientNextOfKin(Guid id, string PhoneNumber, string Name, string Relationship)
        {
            using (var context = new CovidAppContext())
            {
                var result = "";
                await Task.Run(() =>
                {

                    using (var cmd = context.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "dbo.sp_UpdatePatientNextOfKin";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id });
                        cmd.Parameters.Add(new SqlParameter("@phoneNumber", SqlDbType.VarChar) { Value = PhoneNumber });
                        cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar) { Value = Name });
                        cmd.Parameters.Add(new SqlParameter("@relationship", SqlDbType.VarChar) { Value = Relationship });

                        if (cmd.Connection.State != ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }

                        result = cmd.ExecuteScalar().ToString();
                    };
                });

                return result;
            }

        }
    }
}

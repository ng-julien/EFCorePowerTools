﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ConsoleApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public partial class ChinookContext
    {
        private IChinookContextProcedures _procedures;

        public virtual IChinookContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new ChinookContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IChinookContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetTitlesResult>().HasNoKey().ToView(null);
        }
    }

    public partial class ChinookContextProcedures : IChinookContextProcedures
    {
        private readonly ChinookContext _context;

        public ChinookContextProcedures(ChinookContext context)
        {
            _context = context;
        }

        public virtual async Task<List<GetTitlesResult>> GetTitlesAsync(string Title, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Title",
                    Size = 200,
                    Value = Title ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<GetTitlesResult>("EXEC @returnValue = [dbo].[GetTitles] @Title", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}

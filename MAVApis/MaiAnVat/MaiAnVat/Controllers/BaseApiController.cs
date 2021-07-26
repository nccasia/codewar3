using MaiAnVat.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaiAnVat.Controllers
{
    public class Pagination
    {
        public Pagination()
        {
            page = 1;
            rowsPerPage = 10;
            sortBy = null;
            descending = true;
            includeEntities = false;
        }
        public int page { get; set; }
        public int rowsPerPage { get; set; }
        public int records { get; set; }
        public int totalItems { get; set; }
        public int totalPages { get; set; }
        public string sortBy { get; set; }
        public bool descending { get; set; }
        public bool includeEntities { get; set; }
    }

    public class PaginatedResponse<T>
    {
        public Pagination Pagination { get; set; }
        public List<T> Data { get; set; }
    }
    public class BaseApiController : ControllerBase
    {
        protected int UserK
        {
            get
            {
                var userK = 0;
                var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals(MAVClaimTypes.kMAVClaimTypesUserFK, StringComparison.InvariantCultureIgnoreCase))?.Value;
                if (int.TryParse(idClaim, out userK))
                {
                    return userK;
                }
                return 0;
            }
        }
        protected List<string> Roles
        {
            get
            {
                var roles = new List<string>();
                var claimTypeRoles = User.Claims.Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.InvariantCultureIgnoreCase));
                foreach (var role in claimTypeRoles)
                {
                    roles.Add(role.Value);
                }
                return roles;
            }
        }
        protected async Task<PaginatedResponse<T>> GetPaginatedResponse<T>(IQueryable<T> query, Pagination pagination)
        {
            if (pagination == null) pagination = new Pagination();

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pagination.rowsPerPage);

            pagination.page = pagination.page < 1 ? 0 : pagination.page - 1;

            var results = await (pagination.rowsPerPage <= 0
                ? query.ToListAsync()
                : query.Skip(pagination.rowsPerPage * pagination.page)
                    .Take(pagination.rowsPerPage)
                    .ToListAsync());

            var paginationHeader = new Pagination()
            {
                page = pagination.page,
                rowsPerPage = pagination.rowsPerPage,
                records = results.Count,
                totalItems = totalRecords,
                totalPages = totalPages
            };
            HttpContext.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return new PaginatedResponse<T>()
            {
                Pagination = paginationHeader,
                Data = results
            };
        }

    }
}

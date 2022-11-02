using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface ITransactionPointService
    {
        public Task<PagedResponse<List<TransactionPoint>>> getTransactionPoint(PaginationFilter paginationFilter,TransactionPointFilter transactionPointFilter, List<int> unitIds);
        public TransactionPoint createTransactionPoint(TransactionPoint transactionPoint);
        public TransactionPoint updateTransactionPoint(UpdateTransactionPoint updateTransactionPoint, int id);
    }
}

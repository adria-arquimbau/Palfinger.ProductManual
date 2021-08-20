using System.Collections.Generic;
using Palfinger.ProductManual.Queries.Models;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualByProductIdQueryResponse
    {
        public ManualByProductIdPagingResponse ManualByProductIdPagingResponse { get; set; }
    }
}
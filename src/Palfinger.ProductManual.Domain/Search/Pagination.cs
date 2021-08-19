namespace Palfinger.ProductManual.Domain.Search
{
    public class Pagination
    {
        public int PageNumber;

        public int RecordsPerPage;

        public int SkipRecords => (PageNumber - 1) * RecordsPerPage;

        public Pagination(int pageNumber, int recordsPerPage)
        {
            PageNumber = pageNumber;
            RecordsPerPage = recordsPerPage;
        }
    }
}

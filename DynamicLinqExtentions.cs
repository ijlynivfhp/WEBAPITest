using System.Linq.Dynamic.Core;

namespace WEBAPITest
{
    public static class DynamicLinqExtentions
    {
        public static dynamic[] ToDynamicArray(this IQueryable query, DynamicLinqDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Fields))
            {
                query = query.Select($@"new({dto.Fields})");
            }

            if (!string.IsNullOrWhiteSpace(dto.Filter))
            {
                query = query.Where(dto.Filter);
            }

            if (!string.IsNullOrWhiteSpace(dto.OrderBy))
            {
                query = query.OrderBy(dto.OrderBy);
            }

            var pageNo = dto.PageNo ?? 1;
            var pageSize = dto.PageSize ?? 10;
            query = query.Page(pageNo, pageSize);

            return query.ToDynamicArray();
        }
    }
}

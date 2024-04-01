using StackApiDemo.Enums;

namespace StackApiDemo.Parameters
{
    public class TagParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool OrderByAscending { get; set; } = true;
        public OrderByProperties OrderByProperty { get; set; } = OrderByProperties.name;
    }
}

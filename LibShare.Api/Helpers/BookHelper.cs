using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Helpers
{
    public static class BookParameters
    {
        public static SortOrder SortOrder { get; set; } = SortOrder.Id;
        public static int PageSize { get; set; } = 10;
        public static int PageNumber { get; set; } = 1;
        public static bool OnlyEbooks { get; set; } = false;
        public static bool OnlyRealBooks { get; set; } = false;
    }

    public enum SortOrder
    {
        Id,
        Title,
        Author,
        Publisher,
        Year,
        Language,
        Description,
        Category,
        User,
        DateCreate,
        LookedRate
    }
}

using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace PostModule.Domain.UserPostAgg
{
    public class UserPost : BaseEntity<int>
    {
        public UserPost(long userId, int count, string apiCode)
        {
            UserId = userId;
            Count = count;
            ApiCode = apiCode;
        }
        public void UseApi()
        {
            Count--;
        }
        public void CountPlus(int count)
        {
            Count = Count + count;
        }
        public long UserId { get; private set; }
        public int Count { get; private set; }
        public string ApiCode { get; private set; }
    }
}

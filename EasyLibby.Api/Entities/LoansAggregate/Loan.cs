using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Api.Entities.MemberAggregate;

namespace EasyLibby.Api.Entities.LoansAggregate
{
    public class Loan : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int RenewedTimes { get; set; } = 0;
    }
}

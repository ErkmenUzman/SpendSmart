using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expenses
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; } = string.Empty;

    }
}

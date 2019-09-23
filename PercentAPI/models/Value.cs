using System;

namespace PercentAPI.models
{
    public class Value
    {
        public string FinanceType { get; set; }
        public int Installments { get; set; }
        public double TotalAmount { get; set; }
        public DateTime FirstExpiry { get; set; }
    }
}
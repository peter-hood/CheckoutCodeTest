using System;

namespace CheckoutAPI.AcquiringBank
{
    public class BankResponseDto
    {
        public int id { get; private set; }
        public bool status { get; private set; }

        public BankResponseDto(int id, bool status)
        {
            this.id = id;
            this.status = status;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is BankResponseDto other)) return false;
            return (id == other.id) && (status == other.status);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() + status.GetHashCode();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CustomerAddress: IEntityTypeConfiguration<CustomerAddress>
    {
        public int Id { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public bool IsDefault { get; set; } = false;

        public string? UserId { get; set; }
        public UserCustomer? User { get; set; }

        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.HasOne(ca => ca.User)
                   .WithMany(u => u.Addresses)
                   .HasForeignKey(ca => ca.UserId);
        }
    }
}

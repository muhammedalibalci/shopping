using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}

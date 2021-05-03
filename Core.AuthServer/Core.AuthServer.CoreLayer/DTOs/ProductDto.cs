using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AuthServer.CoreLayer.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public int Stock { get; set; }

        public Decimal TotalPrice => Price * Stock;

        public string UserId { get; set; }
    }
}

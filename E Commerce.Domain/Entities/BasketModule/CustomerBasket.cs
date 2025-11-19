using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketModule
{
    //1, 2 ,, 3, 4, 5 ,,7
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}

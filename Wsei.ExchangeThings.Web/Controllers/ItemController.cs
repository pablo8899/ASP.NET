using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wsei.ExchangeThings.Web.Database;
using Wsei.ExchangeThings.Web.Entities;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ExchangesDbContext _dbContext;

        public ItemController(ExchangesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddNewItemResponseModel<DbSet<ItemEntity>> Post(ItemModel item) {
            bool isValid = !string.IsNullOrEmpty(item.Description) && !string.IsNullOrEmpty(item.Name);
            string message = isValid ? "" : "Input fields cannot be empty";

            if(isValid)
            {
                var entity = new ItemEntity
                {
                    Name = item.Name,
                    Description = item.Description,
                    IsVisible = item.IsVisible
                };

                _dbContext.Items.Add(entity);
                _dbContext.SaveChanges();
            }

            var res = new AddNewItemResponseModel<DbSet<ItemEntity>>
            {
                success = isValid,
                message = message,
                data = isValid ? _dbContext.Items : null
            };

            return res;
        }
    }
}

﻿using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.Product
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_PRODUCTS)]
    [PrimaryKeyName("ProductId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Product : DataObject
    {
        public int ProductId { get; set; }
	    public string Name { get; set; }
	    public string Description { get; set; }
	    public int SellerId { get; set; }
	    public decimal AskingPrice { get; set; }
	    public int ProductStatusId { get; set; }
	    public string Brand { get; set; }
	    public string Size { get; set; }
	    public int ProductTypeId { get; set; }
	    public int ConditionId { get; set; }
	    public DateTime CreatedDate { get; set; }
	    public DateTime UpdatedDate { get; set; }

        public Product() { }

        public Product(int id) : base(id) {  }

        public override void Load(int id)
        {
            Product temp = GetByValue<Product>("ProductId", Convert.ToString(id), Constants.TABLE_PRODUCTS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ProductId = temp.ProductId;
                Name = temp.Name;
                Description = temp.Description;
                SellerId = temp.SellerId;
                AskingPrice = temp.AskingPrice;
                ProductStatusId = temp.ProductStatusId;
                Brand = temp.Brand;
                Size = temp.Size;
                ProductTypeId = temp.ProductTypeId;
                ConditionId = temp.ConditionId;
                CreatedDate = temp.CreatedDate;
                UpdatedDate = temp.UpdatedDate;
            }
            else
            {
                throw new Exception("Could not find product with Id " + id);
            }
        }
		
		// This might be best moved into a different class later on.
		public static List<Product> GetProductsForUser(int userId)
		{
			List<Product> result = new List<Product>();

            SearchEntity search = new SearchEntity();

			search.ColumnsToReturn = new List<string>();
			search.ColumnsToReturn.Add("ProductId");
			
            search.ConnectionString = Constants.CONNECTION_STRING;

            LacesRepo.Condition userIdCond = new LacesRepo.Condition();
            userIdCond.Column = "SellerId";
            userIdCond.Operator = LacesRepo.Condition.Operators.EqualTo;
            userIdCond.Value = Convert.ToString(userId);

            LacesRepo.Condition statusCond = new LacesRepo.Condition();
            statusCond.Column = "ProductStatusId";
            statusCond.Operator = LacesRepo.Condition.Operators.EqualTo;
            statusCond.Value = Convert.ToString((int)ProductStatusOptions.Removed);
            statusCond.Invert = true;

            search.Conditions.Add(userIdCond);
            search.Conditions.Add(statusCond);

            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_PRODUCTS;

            result = new GenericRepository<Product>().Read(search);

            return result;
		}
    }
}

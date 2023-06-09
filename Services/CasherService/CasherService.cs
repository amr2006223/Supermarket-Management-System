﻿using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories;
using Supermarket_Managment_System.Repositories.BillRepository;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.CasherService
{
    public class CasherService : ICasherService
    {
        private readonly ICasherRepository _casherRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProductsRepository _productRepository;
        private readonly IBillRepository _billRepository;



        public CasherService(ICasherRepository casherRepository, IBillRepository billRepository, IProductsRepository productRepository, ICategoriesRepository categoriesRepository)
        {
            _casherRepository = casherRepository;
            _billRepository = billRepository;
            _productRepository = productRepository;
            _categoriesRepository = categoriesRepository;

    }

        public IEnumerable<ProductsToBillVM> GetProductsWithCategories()
        {
            var products = _productRepository.GetProducts();
            var categories = _categoriesRepository.GetCategories();

            List<ProductsToBillVM> productsToBillVM = new List<ProductsToBillVM>();

            foreach (var product in products)
            {
                var productCategory = _productRepository.GetProductCategory(product.Id);

                if (productCategory != null)
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                        category = productCategory.Category
                    };
                    productsToBillVM.Add(productsToBill);
                }
                else
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                    };
                    productsToBillVM.Add(productsToBill);
                }
            }
            return productsToBillVM;
        }

        public Guid GetDefaultPaymentMethodId()
        {
            return _billRepository.GetDefaultPaymentMethodId();
        }

        public void CreateBill(bills bill)
        {
            _billRepository.Createbill(bill);
        }

        public async Task<string> AddProductToBill(Guid productId, Guid billId, int quantity)
        {
            var existingItem = _billRepository.getBillItem(productId, billId);
            if (existingItem != null)
            {
                return "Product already exists in the bill.";
            }

            var bill = _billRepository.getBillById(billId);
            var product = await _productRepository.GetProductById(productId);
            var offer = _productRepository.GetProductOffer(productId);

            bill_items_details billItem = new bill_items_details
            {
                Id = Guid.NewGuid(),
                BillId = billId,
                ProductId = productId,
                Quantity = quantity
            };

            _billRepository.AddProductToBill(billItem);

            if (bill != null)
            {
                if (offer != null)
                {
                    float offerPrice = product.Price * offer.Offer.Discount;
                    bill.TotalPrice += offerPrice * quantity;
                }
                else
                {
                    bill.TotalPrice += product.Price * quantity;
                }

                _casherRepository.SaveChanges();

                return "Product added to the bill successfully.";
            }
            else
            {
                return "Bill ID is null.";
            }
        }

        public IEnumerable<object> GetProductsInBill(Guid billId)
        {
            var productsInBill = _productRepository.GetProductsInBill(billId);
            var productsWithQuantity = productsInBill.Select(b => new
            {
                id = b.Product.Id,
                name = b.Product.Name,
                price = b.Product.Price,
                quantity = b.Quantity
            });

            return productsWithQuantity;
        }

        public async Task<string> DeleteProductFromBill(Guid productId, Guid billId)
        {
            var billItem = _billRepository.getBillItem(productId, billId);
            if (billItem != null)
            {
                _billRepository.RemoveProductFromBill(billItem);

                var bill = _billRepository.getBillById(billId);
                var product = await _productRepository.GetProductById(productId);

                bill.TotalPrice -= product.Price * billItem.Quantity;

                _casherRepository.SaveChanges();

                return "Product removed from the bill successfully.";
            }
            else
            {
                return "Product not found in the bill.";
            }
        }

        public async Task<string> EditProductQuantity(Guid productId, Guid billId, int quantity)
        {
            var billItem = _billRepository.getBillItem(productId, billId);
            if (billItem != null)
            {
                var bill = _billRepository.getBillById(billId);
                var product = await _productRepository.GetProductById(productId);

                bill.TotalPrice -= product.Price * billItem.Quantity;
                bill.TotalPrice += product.Price * quantity;

                billItem.Quantity = quantity;

                _casherRepository.SaveChanges();

                return "Quantity updated successfully.";
            }
            else
            {
                return "Product not found in the bill.";
            }
        }

        public IEnumerable<categories> GetAllCategories()
        {
            return _categoriesRepository.GetCategories();
        }

        public IEnumerable<bills> GetBillsList()
        {
            return _billRepository.GetBillsList();
        }

        public void DeleteBill(Guid id)
        {
            if (id == null)
            {
                throw new Exception.NotFoundException("Bill not found");
            }

            _billRepository.deleteBill(id);
        }
        public IEnumerable<bill_items_details> GetBillItems(Guid billId)
        {
            return _billRepository.GetBillItems(billId);
        }
        public bool UpdateQuantity(Guid billItemId, int quantity)
        {
            return _billRepository.UpdateQuantity(billItemId, quantity);
        }
        public bool DeleteItem(Guid id)
        {
            return _billRepository.DeleteItem(id);
        }
    }
}
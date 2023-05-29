using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.CasherService
{
    public class CasherService : ICasherService
    {
        private readonly ICasherRepository _casherRepository;

        public CasherService(ICasherRepository casherRepository)
        {
            _casherRepository = casherRepository;
        }

        public IEnumerable<ProductsToBillVM> GetProductsWithCategories()
        {
            var products = _casherRepository.GetAllProducts();

            List<ProductsToBillVM> productsToBillVM = new List<ProductsToBillVM>();

            foreach (var product in products)
            {
                var productCategory = _casherRepository.GetProductCategory(product.Id);

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
            return _casherRepository.GetDefaultPaymentMethodId();
        }

        public void CreateBill(bills bill)
        {
            _casherRepository.CreateBill(bill);
        }

        public string AddProductToBill(Guid productId, Guid billId, int quantity)
        {
            var existingItem = _casherRepository.GetBillItem(productId, billId);
            if (existingItem != null)
            {
                return "Product already exists in the bill.";
            }

            var bill = _casherRepository.GetBill(billId);
            var product = _casherRepository.GetProduct(productId);
            var offer = _casherRepository.GetProductOffer(productId);

            bill_items_details billItem = new bill_items_details
            {
                Id = Guid.NewGuid(),
                BillId = billId,
                ProductId = productId,
                Quantity = quantity
            };

            _casherRepository.AddProductToBill(billItem);

            if (billId != null)
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
            var productsInBill = _casherRepository.GetProductsInBill(billId);
            var productsWithQuantity = productsInBill.Select(b => new
            {
                id = b.Product.Id,
                name = b.Product.Name,
                price = b.Product.Price,
                quantity = b.Quantity
            });

            return productsWithQuantity;
        }

        public string DeleteProductFromBill(Guid productId, Guid billId)
        {
            var billItem = _casherRepository.GetBillItem(productId, billId);
            if (billItem != null)
            {
                _casherRepository.RemoveProductFromBill(billItem);

                var bill = _casherRepository.GetBill(billId);
                var product = _casherRepository.GetProduct(productId);

                bill.TotalPrice -= product.Price * billItem.Quantity;

                _casherRepository.SaveChanges();

                return "Product removed from the bill successfully.";
            }
            else
            {
                return "Product not found in the bill.";
            }
        }

        public string EditProductQuantity(Guid productId, Guid billId, int quantity)
        {
            var billItem = _casherRepository.GetBillItem(productId, billId);
            if (billItem != null)
            {
                var bill = _casherRepository.GetBill(billId);
                var product = _casherRepository.GetProduct(productId);

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
            return _casherRepository.GetAllCategories();
        }
    }
}

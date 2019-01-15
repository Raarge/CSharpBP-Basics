using Acme.Common;
using System;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }


        ///<summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product of the order.</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity)   // to return multiple changes, changed this from a bool method to a OperationResult method
        {

            return PlaceOrder(product, quantity, null, null);   // In method chaining the first method calls the last overload method using nulls for last 2 parameters


            //**********************************************************************************************************
            //************* Original Code commented out due to refactor with method chaining ***************************
            //**********************************************************************************************************
            //if (product == null)
            //    throw new ArgumentNullException(nameof(product));
            //if (quantity <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(quantity));

            //var success = false;

            //var orderText = "Order from Acme, Inc" + System.Environment.NewLine +
            //                "Product: " + product.ProductCode + System.Environment.NewLine +
            //                "Quantity: " + quantity;

            //var emailService = new EmailService();
            //var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            //if (confirmation.StartsWith("Message sent:"))
            //{
            //    success = true;
            //}

            //var operationResult = new OperationResult(success, orderText);
            //return operationResult;
            //**********************************************************************************************************
        }

        ///<summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product of the order.</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)   // to return multiple changes, changed this from a bool method to a OperationResult method
        {

            return PlaceOrder(product, quantity, deliverBy, null); // As with the first second calls the last overload method this time passing 3 parameters with the last null


            //**********************************************************************************************************
            //************* Original Code commented out due to refactor with method chaining ***************************
            //**********************************************************************************************************
            //if (product == null)
            //    throw new ArgumentNullException(nameof(product));
            //if (quantity <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(quantity));
            //if (deliverBy <= DateTimeOffset.Now)
            //    throw new ArgumentOutOfRangeException(nameof(deliverBy));

            //var success = false;

            //var orderText = "Order from Acme, Inc" + System.Environment.NewLine +
            //                "Product: " + product.ProductCode + System.Environment.NewLine +
            //                "Quantity: " + quantity;

            //if (deliverBy.HasValue)
            //{
            //    orderText += System.Environment.NewLine + "Deliver By: " + deliverBy.Value.ToString("d");
            //}

            //var emailService = new EmailService();
            //var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            //if (confirmation.StartsWith("Message sent:"))
            //{
            //    success = true;
            //}

            //var operationResult = new OperationResult(success, orderText);
            //return operationResult;
            //***************************************************************************************************************
        }

        ///<summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product of the order.</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <param name="instructions">Instructions for delivery.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy, string instructions)   // to return multiple changes, changed this from a bool method to a OperationResult method
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));
            
            var success = false;

            var orderText = "Order from Acme, Inc" + System.Environment.NewLine +
                            "Product: " + product.ProductCode + System.Environment.NewLine +
                            "Quantity: " + quantity;

            if (deliverBy.HasValue)
            {
                orderText += System.Environment.NewLine + "Deliver By: " + deliverBy.Value.ToString("d");
            }

            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderText += System.Environment.NewLine + "Instructions: " + instructions;
            }

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }

            var operationResult = new OperationResult(success, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }
    }
}

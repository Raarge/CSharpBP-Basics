﻿using Acme.Common;
using System;
using System.Text;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor
    {
        public enum IncludeAddress
        {
            Yes,
            No
        };

        public enum SendCopy
        {
            Yes,
            No
        };


        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }


        /////<summary>
        ///// Sends a product order to the vendor.
        ///// </summary>
        ///// <param name="product">Product of the order.</param>
        ///// <param name="quantity">Quantity of the product to order</param>          
        ///// <returns></returns>
        //public OperationResult PlaceOrder(Product product, int quantity)   // to return multiple changes, changed this from a bool method to a OperationResult method   
        //{

        //    return PlaceOrder(product, quantity, null, null);   // In method chaining the first method calls the last overload method using nulls for last 2 parameters


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
        //}

        /////<summary>
        ///// Sends a product order to the vendor.
        ///// </summary>
        ///// <param name="product">Product of the order.</param>
        ///// <param name="quantity">Quantity of the product to order</param>
        ///// <param name="deliverBy">Requested delivery date.</param>
        ///// <returns></returns>
        //public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)   // to return multiple changes, changed this from a bool method to a OperationResult method
        //{   // Making DateTimeOffSet and instructions optional in the 4 parameter method negates this one all together

        //    return PlaceOrder(product, quantity, deliverBy, null); // As with the first second calls the last overload method this time passing 3 parameters with the last null


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
        //}

        ///<summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product of the order.</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <param name="instructions">Instructions for delivery.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy = null, string instructions = "standard delivery")   // to return multiple changes, changed this from a bool method to a OperationResult method
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));
            
            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" + System.Environment.NewLine +
                            "Product: " + product.ProductCode + System.Environment.NewLine +
                            "Quantity: " + quantity);

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine + "Deliver By: " + deliverBy.Value.ToString("d"));
            }

            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append( System.Environment.NewLine + "Instructions: " + instructions);
            }

            var orderText = orderTextBuilder.ToString();

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
        ///  Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="includeAddress">True to include the shipping address.</param>
        /// <param name="sendCopy">True to send a copy of the email to the customer.</param>
        /// <returns>Success flag and order text</returns>
        public OperationResult PlaceOrder(Product product, int quantity, IncludeAddress includeAddress, SendCopy sendCopy)
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " With Address";
            if (sendCopy == SendCopy.Yes) orderText += " With Copy";

            var operationResult = new OperationResult(true, orderText);
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

        public override string ToString()
        {
            string vendorInfo = "Vendor: " + this.CompanyName;
            string result;
            
                result = vendorInfo?.ToLower();
                result = vendorInfo?.ToUpper();
                result = vendorInfo?.Replace("Vendor", "Supplier");

                var length = vendorInfo?.Length;
                var index = vendorInfo?.IndexOf(":");
                var begins = vendorInfo?.StartsWith("Vendor");

            

            return vendorInfo;
        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define a new line";
            return directions;
        }

        public string PrepareDirectionsOnTwoLines()
        {
            var directions = "First do this" + Environment.NewLine + "Then do that";

            var directions2 = "First do this\r\nThen do that";

            var directions3 = "first do this " +
                              "then do that";
                              

            return directions;
        }
    }
}

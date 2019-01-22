using Acme.Common;
using System;
using static Acme.Common.LoggingService;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;   // will set in constructor

        #region Constructors 
        public Product()
        {
            Console.WriteLine("Product instance created");
            //this.ProductVendor = new Vendor();   // added for always needed related objects for vendor
            // **** above code commented out for lazy loading when only sometimes need related objects
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }

        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " +
                              ProductName);
        }
        #endregion



        #region Properties
        private DateTime? availabilityDate; 

        public DateTime? AvailabilityDate 
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public decimal Cost { get; set; }


        private string productName;

        public string ProductName
        {
            get
            {
                var formattedValue = productName?.Trim();  // use the trim method to remove the beginning and trailing spaces in product name.
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }
                
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        // **** Gives anything creating a product object will have access to the vendor associated with it ****
        private Vendor productVendor;  // creates a field for Always needed Related Objects

        public Vendor ProductVendor
        {
            get
            {
                // **** Code added for lazy loading of productVendor when relate object only needed sometimes ****
                if (productVendor == null)  // check to see if the productVendor is empty
                {
                    productVendor = new Vendor();  // new it up if null, we need it
                }
                // **** Code above added for lazy loading ****

                return productVendor;  // original get statement
            }
            set { productVendor = value; }
        }

        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;

        public string ValidationMessage { get; private set; }

        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";  // using string interpolation
        
        //public string ProductCode => String.Format("{0}-{1:0000}", this.Category, this.SequenceNumber);

        //public string ProductCode => this.Category + "-" + this.SequenceNumber;

        // **** Gives anything creating a product object will have access to the vendor associated with it ****

        #endregion

    
        ///<summary>
        ///Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost.</param>
        /// <return></return>
        public decimal CalculateSuggestedPrice(decimal markupPercent) =>     // Expression-Bodied Method C# 6
            this.Cost + (this.Cost * markupPercent / 100);                   // removed the {}'s added the => (lambda operator)
                                                                             // removed the return keyword before the this.Cost


        // ***** Original Method before refactoring using Expression-Bodied Method from C# 6 *****
        //public decimal CalculateSuggestedPrice(decimal markupPercent)       //******************
        //{                                                                   //******************
        //    return this.Cost + (this.Cost * markupPercent / 100);           //******************
        //}                                                                   //******************
        //****************************************************************************************

        public string SayHello()
        {
            // **** This object only needed here so is only instantiated in the method that needs it ****
            //var vendor = new Vendor();    // Commented out for always needed for always needed related object
            //vendor.SendWelcomeEmail("Message from Product");  //Commented out for always needed related object
            // **** Object created only here because only needed in this one method ****

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");

            var result = LogAction("saying hello");
            
            return "Hello " + ProductName + " (" +
                   ProductId + "): " + Description +
                " Available on: " + AvailabilityDate?.ToShortDateString();
        }

        public override string ToString() => 
            this.ProductName + " (" + this.productId + ")";  // using this with the Product Test Method to 
        // refactored ToString() to an Expression-Bodied Method to reduce lines and clean up

    }
}

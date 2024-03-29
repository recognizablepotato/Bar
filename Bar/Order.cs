//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public string Name { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        public string Quantity { get; set; }
        public int PK { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Drink Drink { get; set; }
    }
}

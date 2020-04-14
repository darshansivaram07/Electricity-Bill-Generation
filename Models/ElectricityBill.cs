using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricityBillGeneration.Models
{
    public class ElectricityBill
    {
        [Key]
        [RegularExpression(@"^[E][B][0-9]{5}$", ErrorMessage = "Enter Valid Customer Number!!!")]
        [Display(Name ="Consumer Number")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ConsumerNumber
        {
            get;
            set;
        }
        [Display(Name ="Consumer Name")]
        public string ConsumerName
        {
            get;
            set;
        }
        [Display(Name ="Units Consumed")]
        public int UnitsConsumed
        {
            get;
            set;
        }
        [Display(Name ="Bill Amount")]
        public double BillAmount
        {
            get;
            set;
        }
    }
}
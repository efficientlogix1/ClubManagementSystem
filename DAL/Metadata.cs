using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   


    public partial class CommonFilters
    {
        public string Search { get; set; }
        public string Active { get; set; }
        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }
        public int CustomerID { get; set; }
        public string Status { get; set; }
    }
    public partial class FetchQuotation_Result
    {
        public string StrCreationOn { get; set; }
    }
    public partial class QuotProduct {
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public int Qty { get; set; }
        public string ContainerVol { get; set; }
        public string Currency { get; set; }
    }
    public partial class Quotation 
    {
        public string StrQuotationDate { get; set; }
        public string StrQuotationDateLong { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorPhone { get; set; }
    }
    public partial class FetchPettyCash_Result 
    {
        public string StrTransactionTime { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public bool IsToaysClosed { get; set; }
    }
    public partial class PettyCash 
    {
        public string StrTransactionTime { get; set; }
    }
    public partial class FetchDailyProduction_Result {
        public string StrDate { get; set; }
    }
    public partial class FetchDailyFinishedGoods_Result
    {
        public string StrDate { get; set; }
    }
    public partial class FetchOrder_Result 
    {
        public string StrCreatedOn { get; set; }
        public string StrDeliveryDate { get; set; }
    }
    public partial class Order 
    {
        public string StrOrderDueDate { get; set; }
        public string StrDeliveryDate { get; set; }
        public string StrDeliveryDate2 { get; set; }
        public string StrDeliveryDate3 { get; set; }
        public string StrDeliveryDate4 { get; set; }
        public string StrDeliveryDate5 { get; set; }
        public string StrDeliveryDate6 { get; set; }
    }
    public partial class DailyFinishedGood 
    {
        public string StrCreationDate { get; set; }
    }
    public partial class InvoiceTracker 
    {
        public string StrDeliveryDate { get; set; }
        public string StrInvoiceDate { get; set; }
    }
    public partial class FetchInvoiceTracker_Result
    {
        public string StrDeliveryDate { get; set; }
        public string StrInvoiceDate { get; set; }
    }
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public List<AttachFiles> AttachFiles { get; set; }
    }

    public class AttachFiles
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int PersonID { get; set; }
    }

    public class FetchPerson_Result
    {
        public Nullable<long> RowNo { get; set; }
        public Nullable<int> Total { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}

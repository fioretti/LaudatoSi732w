using System;
using System.Collections.Generic;

namespace MarcoPoloGCWeb.Models
{
    public partial class Gcoutlet
    {
        public int Id { get; set; }
        public int? GiftCertificateId { get; set; }
        public int? OutletId { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public GiftCertificate GiftCertificate { get; set; }
        public Outlet Outlet { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PR6_Lashkov.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipments()
        {
            this.Shipment_Details = new HashSet<Shipment_Details>();
        }
    
        public int id { get; set; }
        public int order_id { get; set; }
        public System.DateTime shipment_date { get; set; }
        public Nullable<int> status_id { get; set; }
    
        public virtual Orders Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipment_Details> Shipment_Details { get; set; }
        public virtual Statuses Statuses { get; set; }
    }
}

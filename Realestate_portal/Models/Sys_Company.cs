
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Realestate_portal.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Sys_Company
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Sys_Company()
    {

        this.Sys_Users = new HashSet<Sys_Users>();

        this.Tb_Resources = new HashSet<Tb_Resources>();

        this.Tb_Videos = new HashSet<Tb_Videos>();

        this.Tb_Webinars = new HashSet<Tb_Webinars>();

        this.Tb_Docpackages = new HashSet<Tb_Docpackages>();

        this.Tb_Customers = new HashSet<Tb_Customers>();

        this.Tb_Marketing = new HashSet<Tb_Marketing>();

        this.Tb_Network = new HashSet<Tb_Network>();

    }


    public int ID_Company { get; set; }

    public string Name { get; set; }

    public string Logo { get; set; }

    public string Web { get; set; }

    public string ShortName { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Sys_Users> Sys_Users { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Resources> Tb_Resources { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Videos> Tb_Videos { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Webinars> Tb_Webinars { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Docpackages> Tb_Docpackages { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Customers> Tb_Customers { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Marketing> Tb_Marketing { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tb_Network> Tb_Network { get; set; }

}

}

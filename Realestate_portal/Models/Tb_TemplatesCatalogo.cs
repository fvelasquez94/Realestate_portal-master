
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
    
public partial class Tb_TemplatesCatalogo
{

    public int ID_Template { get; set; }

    public string Template_name { get; set; }

    public string Resource { get; set; }

    public int Style { get; set; }

    public string Broker_name { get; set; }

    public decimal Price { get; set; }

    public string Status { get; set; }

    public string Url_image { get; set; }

    public bool visible { get; set; }

    public int ID_Company { get; set; }

    public string htmltext { get; set; }

    public bool original { get; set; }



    public virtual Sys_Company Sys_Company { get; set; }

}

}

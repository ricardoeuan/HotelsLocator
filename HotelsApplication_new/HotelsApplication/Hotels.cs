//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelsApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Hotels
    {
        [Key]
        public int HotelID { get; set; }
        
        [Required]
        public double hotel_latitude { get; set; }
        [Required]
        public double hotel_longitude { get; set; }
        [Required]
        public string hotel_name { get; set; }
        
        public string address { get; set; }
        
        public string phone { get; set; }
        
        public string category { get; set; }
        
        public decimal lowest_rate { get; set; }
        
        public string hotel_description { get; set; }
        public byte[] photo { get; set; }
    }
}
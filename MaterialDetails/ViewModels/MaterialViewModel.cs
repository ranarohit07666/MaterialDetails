using MaterialDetails.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaterialDetails.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public decimal Rate { get; set; }
        public decimal Consumption { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }
        public int ReferenceId { get; set; }

    }
}

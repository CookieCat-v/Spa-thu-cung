using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuanLy.Models
{
    public class Pet
    {
        private string _petName = "";
        private string _ownerName = "";
        [Key]
        private string _petId = "";
        private string _petNote = "";
        private string _phone = "";

        public string PetName { get => _petName; set => _petName = value; }
        public string OwnerName { get => _ownerName; set => _ownerName = value; }
  
        public string PetId { get => _petId; set => _petId = value; }
        public string PetNote { get => _petNote; set => _petNote = value; }
        public string Phone { get => _phone; set => _phone = value; }
    }
}

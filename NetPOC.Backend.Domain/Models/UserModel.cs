using System;
using System.ComponentModel.DataAnnotations;
using NetPOC.Backend.Domain.Enums;
using NetPOC.Backend.Domain.Validations;

namespace NetPOC.Backend.Domain.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "É necessário informar um Nome")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "É necessário informar um Sobrenome")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "É necessário informar um E-mail")]
        [EmailAddress(ErrorMessage = "E-mail não é válido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "É necessário informar uma Data de Nascimento")]
        [CheckBirthDate]
        public DateTime BirthDate { get; set; }
        
        [Required(ErrorMessage = "É necessário informar a Escolaridade")]
        [EnumDataType(typeof(FlagEducation), ErrorMessage = "Este id de escolaridade não é válido")]
        public FlagEducation Education { get; set; }
    }
}
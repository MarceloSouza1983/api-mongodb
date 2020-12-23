using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class InfectadoDto {

        [Required]
        public long id { get; set; }

        public DateTime dataNascimento { get; set; }
        public string sexo { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

}
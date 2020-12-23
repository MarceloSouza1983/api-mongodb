using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections {

    public class Infectado {
        
        public Infectado(long id, DateTime dataNascimento, string sexo, double latitude, double longitude) {
            this.id = id;
            this.dataNascimento = dataNascimento;
            this.sexo = sexo;
            this.localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public long id { get; set; }
        public DateTime dataNascimento { get; set; }
        public string sexo { get; set; }
        public GeoJson2DGeographicCoordinates localizacao { get; set; }
        
    }

}
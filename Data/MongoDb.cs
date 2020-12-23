using System;
using Api.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Api.Data {
    public class MongoDB {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration) {
            try {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();

            } catch (Exception ex) {
                throw new MongoException("Impossível realizar conexão com o banco de dados", ex);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i =>
                {
                    i.AutoMap();
                    //i.MapField(_ => _.dataNascimento).SetElementName("Nascimento");
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
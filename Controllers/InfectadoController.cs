using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB) {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto) {

            long contador = (from i in _infectadosCollection.AsQueryable<Infectado>() select i.id).Count();

            if (contador == 0) {
                contador++;

                Task wait = ComDelay();

                var infectado = new Infectado(contador, dto.dataNascimento, dto.sexo, dto.latitude, dto.longitude);
                _infectadosCollection.InsertOne(infectado);
                return StatusCode((int)HttpStatusCode.Created, "Infectado adicionado com sucesso");
            } else if (contador > 0) {
                long contador1 = (from i in _infectadosCollection.AsQueryable<Infectado>() select i.id).Max();

                contador1++;

                Task wait = ComDelay();

                var infectado = new Infectado(contador1, dto.dataNascimento, dto.sexo, dto.latitude, dto.longitude);
                _infectadosCollection.InsertOne(infectado);
                return StatusCode((int)HttpStatusCode.Created, "Infectado adicionado com sucesso");
            }

            return StatusCode((int)HttpStatusCode.BadRequest, "Erro ao cadastar. Tente novamente");

        }

        [HttpGet]
        public ActionResult ObterInfectados() {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return StatusCode((int)HttpStatusCode.OK, infectados);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarDocumento(long id, [FromBody] InfectadoDto dto) {

            var filter = Builders<Infectado>.Filter.Where(_ => _.id == id);
            var infectado = new Infectado(id, dto.dataNascimento, dto.sexo, dto.latitude, dto.longitude);

            var result = _infectadosCollection.ReplaceOne(filter, infectado);
            return StatusCode((int)HttpStatusCode.Accepted, "Documento atualizado com sucesso");
        }

        [HttpPatch("{id}")]
        public ActionResult AtualizarDataNascimento(long id, [FromBody] InfectadoDto dto) {

            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.id == id), Builders<Infectado>.Update.Set("dataNascimento", dto.dataNascimento));

            return StatusCode((int)HttpStatusCode.Accepted, "Data de nascimento atualizada com sucesso");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) {

            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.id == id));

            return StatusCode((int)HttpStatusCode.Accepted, "Documento deletado com sucesso");
        }

        public int gerarNumeros() {
            Random randNum = new Random(); 
            int numero1 = randNum.Next(0, 50);
            //Console.WriteLine("Numero 1: " + numero1);
            int numero2 = randNum.Next(0, 50);
            //Console.WriteLine("Numero 2: " + numero2);
            int numero3 = randNum.Next(0, 50);
            //Console.WriteLine("Numero 3: " + numero3);

        return numero1 + numero2 + numero3;
        }

        public async Task ComDelay() {
		    var sw = new Stopwatch();
		    sw.Start();
            await Task.Delay(gerarNumeros());
        }

    }

}
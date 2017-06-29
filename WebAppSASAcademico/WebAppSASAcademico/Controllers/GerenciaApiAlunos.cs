using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebAppSASAcademico.Models;

namespace WebAppSASAcademico.Controllers
{
    public class GerenciaApiAlunos
    {
        public Alunos GetAlunoByCpf(Int64 id)
        {
            Alunos alunos = new Alunos();
            //IEnumerable<RegistroAluno> a;//= new List<Aluno>();
            string uri = "http://servicoaluno.portalsas.com.br/api/v1/public/alunos/"+id;
            
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(uri).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        
                        var registros = response.Content.ReadAsAsync<IEnumerable<RegistroAluno>>().Result;
                        alunos = registros.First().alunos;
                        
                    }

                }

            }

            return alunos;
        }
    }
}
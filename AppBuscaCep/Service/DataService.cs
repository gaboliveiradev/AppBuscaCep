﻿using System;
using System.Collections.Generic;
using System.Text;
using AppBuscaCep.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppBuscaCep.Service
{
    public class DataService
    {

        public static async Task<Endereco> GetEnderecoByCep(string cep)
        {
            Endereco end;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/endereco/by-cep?cep=" + cep);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    end = JsonConvert.DeserializeObject<Endereco>(json);
                }
                else throw new Exception(response.RequestMessage.Content.ToString());
            }

            return end;
        }

        public static async Task<List<Bairro>> GetBairrosByIdCidade(int id_cidade)
        {
            List<Bairro> arr_bairros = new List<Bairro>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/endereco/by-cep?cep=" + id_cidade);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    arr_bairros = JsonConvert.DeserializeObject<List<Bairro>>(json);
                }
                else throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_bairros;
        }

        public static async Task<List<Logradouro>> GetLogradourosByBairroAndIdCidade(int id_cidade, string bairro)
        {
            List<Logradouro> arr_logradouros = new List<Logradouro>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://cep.metoda.com.br/logradouro/by-bairro?id_cidade={id_cidade}&bairro={bairro}");

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    arr_logradouros = JsonConvert.DeserializeObject<List<Logradouro>>(json);
                }
                else throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_logradouros;
        }

        public static async Task<List<Cep>> GetCepByLogradouro(string logradouro)
        {
            List<Cep> arr_cep = new List<Cep>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://cep.metoda.com.br/cep/by-logradouro?logradouro={logradouro}");

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    arr_cep = JsonConvert.DeserializeObject<List<Cep>>(json);
                }
                else throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_cep;
        }
    }
}

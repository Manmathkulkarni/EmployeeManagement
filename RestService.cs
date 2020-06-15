using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using EmployeeManagement.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public static class RestService
    {
        const string rootUrl = "http://dummy.restapiexample.com/";
        const string create = "create";
        const string delete = "delete";
        const string getAll = "employee";
        
        public static async Task<ShowEmployee> GetAllEmployees()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("api/v1/employees");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<ShowEmployee>().Result;
                }
                return null;
            }
        }

        public static async Task<ShowEmployee> GetEmployee(int id)
        {
            string absolutePath = "api/v1/employee/" + id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(absolutePath);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<ShowEmployee>().Result;
                }
                return null;
            }

        }

        public static async Task<CreateEmployee> CreateEmployee(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(employee));

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/v1/create", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<CreateEmployee>().Result;
                }
                return null;
            }
        }

        public static async Task<CreateEmployee> UpdateEmployee(int id, Employee employee)
        {
            string absolutePath = "api/v1/update/" + id.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(employee));

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(stringPayload, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<CreateEmployee>().Result;
                }
                return null;
            }
        }

        public static async Task<DeleteEmployee> DeleteEmployee(int id)
        {
            string absolutePath = "api/v1/delete/" + id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(absolutePath);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<DeleteEmployee>().Result;
                }
                return null;
            }
        }
    }
}

using Acudir.Test.Apis.Models;
using Acudir.Test.Apis.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Acudir.Test.Apis.Repository
{
   
        public class PersonaRepository : IPersonaRepository
        {
            private readonly string _filePath = "Test.json";

            public IEnumerable<Persona> GetAll(string nombre, string apellido, int? edad)
            {
                if (!File.Exists(_filePath)) return new List<Persona>();

                var jsonData = File.ReadAllText(_filePath);
                if (string.IsNullOrEmpty(jsonData)) return new List<Persona>();

                try
                {
                    var personas = JsonConvert.DeserializeObject<List<Persona>>(jsonData);
                    return personas
                        .Where(p => (string.IsNullOrEmpty(nombre) || p.Nombre == nombre) &&
                                    (string.IsNullOrEmpty(apellido) || p.Apellido == apellido) &&
                                    (!edad.HasValue || p.Edad == edad))
                        .ToList();
                }
                catch (JsonException)
                {
                    // Handle JSON deserialization error (e.g., log it)
                    return new List<Persona>();
                }
            }

            public Persona GetById(int id)
            {
                if (!File.Exists(_filePath)) return null;

                var jsonData = File.ReadAllText(_filePath);
                if (string.IsNullOrEmpty(jsonData)) return null;

                try
                {
                    var personas = JsonConvert.DeserializeObject<List<Persona>>(jsonData);
                    return personas?.FirstOrDefault(p => p.Id == id);
                }
                catch (JsonException)
                {
                    // Handle JSON deserialization error (e.g., log it)
                    return null;
                }
            }

            public void Add(Persona persona)
            {
                var personas = GetAll(null, null, null).ToList();
                personas.Add(persona);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(personas));
            }

            public void Update(Persona persona)
            {
                var personas = GetAll(null, null, null).ToList();
                var index = personas.FindIndex(p => p.Id == persona.Id);
                if (index >= 0)
                {
                    personas[index] = persona;
                    File.WriteAllText(_filePath, JsonConvert.SerializeObject(personas));
                }
            }

            public void Delete(int id)
            {
                var personas = GetAll(null, null, null).ToList();
                var persona = personas.FirstOrDefault(p => p.Id == id);
                if (persona != null)
                {
                    personas.Remove(persona);
                    File.WriteAllText(_filePath, JsonConvert.SerializeObject(personas));
                }
            }
        }
}

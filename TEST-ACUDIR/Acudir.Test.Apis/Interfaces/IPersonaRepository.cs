using Acudir.Test.Apis.Models;

namespace Acudir.Test.Apis.Interfaces
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetAll(string nombre, string apellido, int? edad);
        Persona GetById(int id);
        void Add(Persona persona);
        void Update(Persona persona);
        void Delete(int id);
    }
}

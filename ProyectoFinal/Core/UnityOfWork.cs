using Persistence;
using Core.Base;
using System;
using Models.Entities;
using Core.Repositories;
using System.Threading.Tasks;

namespace Core
{
    public class UnityOfWork : IDisposable
    {
        private readonly SVContext context;
        public UnityOfWork(SVContext context)
        {
            this.context = context;
            this.OnCreating();
        }

        public CandidatosRepository CandidatosRepository{get; set;}
        public CiudadanoRepository CiudadanoRepository{get; set;}
        public EleccionesRepository EleccionesRepository{get; set;}
        public PartidosRepository PartidosRepository{get;set;}
        public PuestosRepository PuestosRepository{get; set;}
        public VotacionRepository VotacionRepository{get; set;}

        public virtual void OnCreating()
        {
            var type = this.GetType();
            var properties = type.GetProperties();
            foreach(var item in properties)
            {
                var t = item.PropertyType;
                var ctor = t.GetConstructors();
                var instance = Activator.CreateInstance(t,this.context);
                item.SetValue(this, instance);
            }
        }

        public async Task Commit()
        {
            await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
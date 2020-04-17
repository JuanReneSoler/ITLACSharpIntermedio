using Tools;
using Models.Dtos;
using System.Linq;

namespace Models.Entities
{
    public partial class Ciudadanos
    {
        public static explicit operator CiudadanoRequest(Ciudadanos v)
        {
            return new CiudadanoRequest {
                Id = v.Id,
                DocIdentidad = v.DocIdentidad.Replace("-", ""),
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                Email = v.Email,
                //estado = v.Estado,
            };
        }
        public static explicit operator CiudadanoListResponse(Ciudadanos v)
        {
            return new CiudadanoListResponse
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                Estado = v.Estado.Value ? "Activo" : "Inactivo"
            };
        }

        public static explicit operator Ciudadanos(CiudadanoRequest v)
        {
            return new Ciudadanos {
                Id = v.Id,
                DocIdentidad = v.DocIdentidad.Replace("-", ""),
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                Email = v.Email,
            };
        }
    }

    public partial class Votantes
    {
        public static explicit operator Votantes(VotacionRequest v)
        {
            return new Votantes
            {

                Id = v.Id,
                CiudadanoId = v.CiudadanoId,
                EleccionId = v.EleccionId,
                //Fecha = v.Fecha
            };
        }
    }
    
    public partial class Partidos
    {
        public static explicit operator PartidosListResponse(Partidos v)
        {
            return new PartidosListResponse {
                Id = v.Id,
                Nombre = v.Nombre,
                Logo = v.LogoPartido.Base64FromPath(TypeImage.Partido),
                Estado = v.Estado.Value ? "Activo" : "Inactivo",
                
            };
        }
        public static explicit operator Partidos(PartidoRequest v)
        {
            return new Partidos {
                Id = v.Id,
                Nombre = v.Nombre,
                Descripcion = v.Descripcion,
                LogoPartido = v.Logo.SaveToDisk(v.OldPath),
                //Estado= v.estado
            };
        }
        public static explicit operator PartidoRequest(Partidos v)
        {
            return new PartidoRequest {
                Id = v.Id,
                Nombre = v.Nombre,
                Descripcion = v.Descripcion,
                //estado = v.Estado,
                LogoBase64 = v.LogoPartido.Base64FromPath(TypeImage.Partido),
                OldPath = v.LogoPartido
            };
        }
    }

    public partial class PuestosElectivos
    {
        public static explicit operator PuestosListResponse(PuestosElectivos v)
        {
            return new PuestosListResponse {
                Estado = v.Estado.Value ? "Activo" : "Inactivo",
                Id = v.Id,
                Nombre = v.Nombre
            };
        }

        public static explicit operator PuestosElectivos(PuestoRequest v)
        {
            return new PuestosElectivos {
                Descripcion = v.Descripcion,
                Id = v.Id,
                Nombre = v.Nombre,
            };
        }

        public static explicit operator PuestoRequest(PuestosElectivos v)
        {
            return new PuestoRequest {
                Descripcion = v.Descripcion,
                Id = v.Id,
                Nombre = v.Nombre
            };
        }
        public static explicit operator ResultadoResponse(PuestosElectivos v)
        {
            return new ResultadoResponse
            {
                PuestoName = v.Nombre,
                Candidatos = v.Candidatos.Select(x => (CandidatosListResponse)x).ToList()
            };
        }
    }

    public partial class Elecciones
    {
        public static explicit operator EleccionesListResponse(Elecciones v)
        {
            return new EleccionesListResponse
            {
                Estado = v.EstadosId,
                EstadoDescripcion = v.Estados.DescripcionEstado,
                Fecha = v.FechaRealizadion.ToString("dd/MM/yyyy"),
                Id = v.Id,
                Nombre = v.Nombre,
                Votos = v.Votos.Count
            };
        }

        public static explicit operator Elecciones(EleccionesRequest v)
        {
            return new Elecciones
            {
                FechaRealizadion = v.Fecha,
                Id = v.Id,
                Nombre = v.Nombre,
            };
        }

        public static explicit operator EleccionesRequest(Elecciones v)
        {
            return new EleccionesRequest
            {
                Fecha = v.FechaRealizadion,
                Id = v.Id,
                Nombre = v.Nombre
            };
        }
    }

    public partial class Candidatos
    {
        public static explicit operator CandidatosListResponse(Candidatos v)
        {
            return new CandidatosListResponse
            {
                Id = v.Id,
                NombreCompleto = v.Ciudadano.Nombre+" "+v.Ciudadano.Apellido,
                Foto = v.FotoPerfil.Base64FromPath(TypeImage.Candidato),
                Partido = v.Partido.Nombre,
                Puesto = v.Puesto.Nombre,
                Estado = v.Estado.Value ? "Activo" : "Inactivo",
                PartidoFoto = v.Partido.LogoPartido.Base64FromPath(TypeImage.Partido),
                Votos = v.Votos != null ? v.Votos.Count() : 0,
            };
        }

        public static explicit operator CandidatosRequest(Candidatos v)
        {
            return new CandidatosRequest
            {
                CiudadanoId = v.CiudadanoId,
                CiudadanoText = v.Ciudadano.Nombre+" "+v.Ciudadano.Apellido,
                FotoBase64 = v.FotoPerfil.Base64FromPath(TypeImage.Candidato),
                Id = v.Id,
                PartidoId = v.PartidoId.Value,
                PartidoText = v.Partido.Nombre,
                PuestoId = v.PuestoId.Value,
                PuestoText = v.Puesto.Nombre,
                OldPath = v.FotoPerfil,
                EleccionId = v.EleccionId
            };
        }

        public static explicit operator Candidatos(CandidatosRequest v)
        {
            return new Candidatos
            {
                CiudadanoId = v.CiudadanoId,
                EleccionId = v.EleccionId,
                FotoPerfil = v.Foto.SaveToDisk(v.OldPath),
                Id = v.Id,
                PartidoId = v.PartidoId,
                PuestoId = v.PuestoId,
            };
        }
    }
}
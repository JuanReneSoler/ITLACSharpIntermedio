
namespace Tareas.Models
{
    public class Elemento
    {
        public string Simbolo{get; set;}
        public string Nombre { get; set; }
        public int Grupo{get; set;}
        public int Periodo{get; set;}
        public SubTipo? Tipo{get; set;}
    }

    // public enum Tipo
    // {
    //     Metales,
    //     Metaloides,
    //     NoMetales
    // }

    public enum SubTipo
    {
        Alcalinos,
        Alcalinos_Terreos,
        Lantinidos,
        Actinidos,
        Metales_Transicion,
        OtrosMetales,
        OtrosNoMetales,
        Halogenos,
        GasesNobles,
        Metaloides,
    }
}

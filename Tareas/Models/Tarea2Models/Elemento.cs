
namespace Tareas.Models
{
    public class Elemento
    {
        string Simbolo{get; set;}
        int Grupo{get; set;}
        int Periodo{get; set;}
        Tipo Tipo{get; set;}
        string RGBColor{get;set;}
    }

    public enum Tipo
    {
        Metales,
        Metaloides,
        NoMetales
    }

    public enum SubTipo
    {
        Alcalinos,
        Alcalinos_Terreos,
        Lantanidos,
        Actinidos,
        Metales_Transicion,
        OtrosMetales,
        OtrosNoMetales,
        Halogenos,
        GasesNobles
    }
}


using Tareas.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
namespace Tareas.CtxTarea4
{
    public partial class Pokemon
    {
        public static explicit operator PokemonListResponse(Pokemon v)
        {
            if(
                v.RegionPokemon == null 
                || !v.RegionPokemon.Any()
                || v.TipoPokemon == null
                || !v.TipoPokemon.Any()
                || v.Ataques == null
                || !v.Ataques.Any())
                throw new System.Exception("Las lista de regiones y tipo puede que esten vacias.");
            
            var listRegion = new List<RegionesListResponse>();
            foreach(var item in v.RegionPokemon)
            {
                listRegion.Add(new RegionesListResponse
                {
                    Id = item.RegionId,
                    Nombre = item.Region.Nombre,
                    RGB = item.Region.Rgbcolor
                });
            }

            var listTipo = new List<TipoListResponse>();
            foreach(var item in v.TipoPokemon)
            {
                listTipo.Add(new TipoListResponse
                {
                    Id = item.TipoId,
                    Nombre = item.Tipo.Nombre,
                    RGB = item.Tipo.Rgbcolor
                });
            }

            var result = new PokemonListResponse
            {
                Id = v.Id,
                //FotoBase64 = base64,
                Nombre = v.Nombre,
                Regiones = listRegion.ToArray(),
                Tipos = listTipo.ToArray(),

            };

            if(v.FotoPath != null && !System.IO.File.Exists(v.FotoPath))
                throw new System.Exception("Este archivo no existe.");

            if(v.FotoPath != null)
            {
                var byteArr = System.IO.File.ReadAllBytesAsync(v.FotoPath).Result;
                var base64 = System.Convert.ToBase64String(byteArr);
                result.FotoBase64 = base64;
            }

            return result;
        }

        public static explicit operator Pokemon(PokemonRequest v)
        {
            if(
                v.Ataques == null
                || !v.Ataques.Any()
                || v.RegionesId == null
                || !v.RegionesId.Any()
                || v.TiposId == null
                || !v.TiposId.Any()
            )
                throw new Exception("Debe especificar ataques, regiones y tipos a este pokemon.");
            
            var result = new Pokemon
            {
                Id = v.Id,
                FotoPath = v.FotoPath,
                Nombre = v.Nombre,
                Ataques = new List<Ataques>(),
                RegionPokemon = new List<RegionPokemon>(),
                TipoPokemon = new List<TipoPokemon>()
            };
            
            if(v.Foto != null)
            {
                var stringPath = "C:\\Pokedex\\";
                System.IO.Directory.CreateDirectory(stringPath);

                if(!System.IO.File.Exists(stringPath+v.Foto.FileName))
                    using(var stream = new System.IO.FileStream(stringPath+v.Foto.FileName, System.IO.FileMode.Create))
                    {
                        v.Foto.CopyToAsync(stream);
                    }

                result.FotoPath = stringPath+v.Foto.FileName;
            }

            using(var ctx = new Tarea4Context())
            {
                foreach(var item in v.Ataques)
                {
                    if(!ctx.Ataques.Any(x=>x.Nombre == item && x.PokemonId == v.Id))
                        result.Ataques.Add(new Ataques
                        {
                            Id = 0,
                            Nombre = item,
                            PokemonId = v.Id
                        });
                }

                foreach(var item in v.RegionesId)
                {
                    if(!ctx.RegionPokemon.Any(x=>x.PokemonId == v.Id && x.RegionId == item))
                        result.RegionPokemon.Add(new RegionPokemon
                        {
                            Id = 0,
                            PokemonId = v.Id,
                            RegionId = item
                        });
                }

                foreach(var item in v.TiposId)
                {
                    if(!ctx.TipoPokemon.Any(x=>x.PokemonId == v.Id && x.TipoId == item))
                        result.TipoPokemon.Add(new TipoPokemon
                        {
                            Id = 0,
                            PokemonId = v.Id,
                            TipoId = item,
                        });  
                }
            }

            return result;
        }

        public static explicit operator PokemonRequest(Pokemon v)
        {
            if(
                v.RegionPokemon == null
                || !v.RegionPokemon.Any()
                || v.TipoPokemon == null
                || !v.TipoPokemon.Any()
                || v.Ataques == null
                || !v.Ataques.Any()
                )
                throw new System.Exception("");

            var listRegiones = new List<dynamic>();
            foreach(var item in v.RegionPokemon)
            {
                listRegiones.Add(new 
                {
                    Id = item.RegionId,
                    Nombre = item.Region.Nombre,
                    RGB = item.Region.Rgbcolor
                });
            }

            var listTipos = new List<dynamic>();
            foreach(var item in v.TipoPokemon)
            {
                listTipos.Add(new
                {
                    Id = item.TipoId,
                    Nombre = item.Tipo.Nombre,
                    RGB = item.Tipo.Rgbcolor
                });
            }

            var listAtaques = new List<dynamic>();
            foreach(var item in v.Ataques)
            {
                listAtaques.Add(new 
                {
                    Id = item.Id,
                    Nombre = item.Nombre
                });
            }
            
            var result = new PokemonRequest
            {
                Id = v.Id,
                Nombre = v.Nombre,
                //Foto,
                AtaquesObj = listAtaques,
                RegionesObj = listRegiones,
                TiposObj = listTipos,
                FotoPath = v.FotoPath
            };


            if(v.FotoPath != null)
            {
                var byteArr = System.IO.File.ReadAllBytesAsync(v.FotoPath).Result;
                var base64 = System.Convert.ToBase64String(byteArr);
                result.FotoBase64 = base64;
            }

            return result;
        }
    }

    public partial class Region
    {
        public static explicit operator Region(RegionesListResponse v)
        {
            return new Region
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Rgbcolor = v.RGB
            };
        }
        public static explicit operator RegionesListResponse(Region v)
        {
            return new RegionesListResponse
            {
                Id = v.Id,
                Nombre = v.Nombre,
                RGB = v.Rgbcolor
            };
        }
    }

    public partial class Tipo
    {
        public static explicit operator Tipo(TipoListResponse v)
        {
            var result = new Tipo
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Rgbcolor = v.RGB,
                FotoPath = v.FotoPath
            };

            if(v.Foto != null)
            {
                var stringPath = "C:\\Pokedex\\";
                System.IO.Directory.CreateDirectory(stringPath);

                if(!System.IO.File.Exists(stringPath+v.Foto.FileName))
                    using(var stream = new System.IO.FileStream(stringPath+v.Foto.FileName, System.IO.FileMode.Create))
                    {
                        v.Foto.CopyToAsync(stream);
                    }

                result.FotoPath = stringPath+v.Foto.FileName;
            }

            return result;
        }
        public static explicit operator TipoListResponse(Tipo v)
        {
            var result = new TipoListResponse
            {
                Id = v.Id,
                Nombre = v.Nombre,
                RGB = v.Rgbcolor,
                //FotoBase64 = base64
            };
            if(v.FotoPath != null)
            {
                var byteArr = System.IO.File.ReadAllBytesAsync(v.FotoPath).Result;
                var base64 = System.Convert.ToBase64String(byteArr);
                result.FotoBase64 = base64;
            }
            
            return result;
        }
    }
}




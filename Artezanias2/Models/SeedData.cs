using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Artezanias2.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new Artezanias2Context
               (serviceProvider.GetRequiredService<DbContextOptions<Artezanias2Context>>()))
            {
                if (context.Pages.Any())
                {
                    return;
                }
                context.Pages.AddRange(new Page
                {
                    Titulo = "Inicio", //Inicio
                    Slug = "Inicio",
                    Contenido = "Página de inicio",
                    Clasificación = 100
                },
                 new Page
                 {
                     Titulo = "Sobre nosotros ", //Acerca de nosotros
                     Slug = "Sobre nosotros",
                     Contenido = "Sobre la página",
                     Clasificación = 100
                 },

                new Page
                {
                    Titulo = "Servicios", //Tienda Tenis
                    Slug = "servicio",
                    Contenido = "Página de Servicios ",
                    Clasificación = 100
                },

                new Page
                {
                    Titulo = "Contacto",
                    Slug = "contacto",
                    Contenido = "Página de contacto",
                    Clasificación = 100
                }


                    );
                context.SaveChanges();
            }


        }



        }





    }


using Fiap.Ingresso.Evento.API.Domain;
using System;
using domain = Fiap.Ingresso.Evento.API.Domain;

namespace Fiap.Ingresso.Evento.API.Data.Seed;

public static class EventoSeed
{
    public static void Seed(EventoContext context)
    {
        if (!context.Eventos.Any())
        {
            context.AddRange(CriaEventos());
            context.SaveChanges();
        }
    }

    public static List<domain.Evento> CriaEventos()
    {
        List<domain.Evento> eventos = new List<domain.Evento>();
        
        domain.Evento eventoNovo = new domain.Evento();
        eventoNovo.AdicionarEvento("evento seed", DateTime.Now, DateTime.Now.AddDays(10), DateTime.Now.AddDays(20), 20, "endereco", "20", "Bauru", "bairro", "17120352", 300);
        if (!eventoNovo.Erros.Any())
        {
            eventos.Add(eventoNovo);
        }

        return eventos;
    }
}

﻿using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Domain.Persistence;

namespace NextMidiaWeb.Domain.Services
{
    public class MidiaFavoritadaService
    {
        private readonly MidiaFavoritadaDbContext _context;

        public MidiaFavoritadaService(MidiaFavoritadaDbContext midiaFavDbContext)
        {
            this._context = midiaFavDbContext;
        }

        public void Create(MidiaFavoritada fav)
        {
            _context.midia_Favoritada.Add(fav);
            _context.SaveChanges();
        }

        public MidiaFavoritada GetById(long idMidia, long idUsuario)
        {
            return _context.midia_Favoritada                
                .SingleOrDefault(s => s.Midia_Id == idMidia && s.Usuario_Id == idUsuario);
        }
    }
}
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Domain.Persistence;
using System.Data;

namespace NextMidiaWeb.Domain.Services
{
    public class ComentarioService
    {
        #region Private Properties
        private readonly ComentarioDbContext _context;
        #endregion

        #region Constructors
        public ComentarioService(ComentarioDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public void Create(Comentario comentario)
        {
            int id = this._context.comentario.Count() > 0 ? 
                this
                ._context
                .comentario
                .OrderByDescending(com => com.Id)
                .FirstOrDefault()
                .Id + 1 : 1;

            comentario.Id = id;
            this._context.comentario.Add(comentario);
            this._context.SaveChanges();
        }

        public List<Comentario> GetCommentList(int idMidia)
        {
            return _context.comentario.Where(com => com.Midia_Id == idMidia).ToList();
        }
        #endregion
    }
}

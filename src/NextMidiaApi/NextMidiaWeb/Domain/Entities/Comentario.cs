using NextMidiaWeb.Models.Input;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextMidiaWeb.Domain.Entities
{
    public class Comentario
    {
        #region Properties
        public int Id { get; set; }
        public int Midia_Id { get; set; }
        public int Usuario_Id { get; set; }      
        public string Texto { get; set; }
        public DateTime Data { get; set; }
        public int Nota { get; set; }
        public bool Is_Deleted { get; set; }
        #endregion

        #region Constructors
        public Comentario()
        {
            this.Id = 0;
            this.Data = DateTime.Now;
            this.Usuario_Id = 0;
            this.Midia_Id = 0;
            this.Texto = string.Empty;
            this.Nota = 0;
            this.Is_Deleted = false;
        }        
        #endregion

        #region Methods
        public void Update(ComentarioInput input)
        {
            Texto = input.Texto;
            Nota = input.Nota;
        }

        public void Delete()
        {
            Is_Deleted = true;
        }
        #endregion        
    }
}

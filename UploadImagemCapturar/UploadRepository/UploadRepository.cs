using UploadImagemCapturar.DataContext;
using UploadImagemCapturar.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UploadImagemCapturar.UploadRepository
{
    public class UploadRepository : IUploadRepository   
    {
        private readonly UploadContext  _context;   
        public  UploadRepository(UploadContext context)
        {
            _context = context;
        }

        public FileUpload RecuperarImagem(int id)
        {
            var imagem = _context.FileUpload.Find(id);
            return imagem;
        }
        public FileUpload RecuperarArquivo(int id)
        {
            var arquivo = _context.FileUpload.Find(id);
            return arquivo;
        }

        public void SalvarImagem(FileUpload image)
        {
            _context.FileUpload.Add(image);
            _context.SaveChanges();
        }
       
    }
}

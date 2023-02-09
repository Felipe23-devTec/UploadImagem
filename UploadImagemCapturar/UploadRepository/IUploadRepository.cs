using UploadImagemCapturar.Models;

namespace UploadImagemCapturar.UploadRepository
{
    public interface IUploadRepository
    {
        public void SalvarImagem(FileUpload image);
        public FileUpload RecuperarImagem(int id);
        public FileUpload RecuperarArquivo(int id);
    }
}

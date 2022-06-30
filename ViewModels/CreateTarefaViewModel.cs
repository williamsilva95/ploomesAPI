using System.ComponentModel.DataAnnotations;

namespace PloomesAPI.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required] //Campo requerido
        public string Title { get; set; }
    }
}
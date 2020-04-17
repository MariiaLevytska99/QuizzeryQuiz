namespace Quizz.ServiceModels
{
    public class LoginModel
    {
        public string AuthToken { get; set; }
        public string Principal { get; set; }
        public string Email { get; set; }
    }
}

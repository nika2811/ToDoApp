namespace ToDoApp.Models.Requests;

public class ResetPasswordRequest
{
    public int UserID { get; set; }
    public string PasswordResetToken { get; set; }
    public string Password { get; set; }
}
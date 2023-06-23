namespace Contracts;

public class AuthorInfoDto
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }

    public AuthorInfoDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccess = false;
    }

    public AuthorInfoDto(string firstname, string lastName)
    {
        Firstname = firstname;
        Lastname = lastName;
        IsSuccess = true;
    }
}
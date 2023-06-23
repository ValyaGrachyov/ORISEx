namespace Contracts;

public class ResultDto
{
    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }

    public ResultDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccess = false;
    }

    public ResultDto()
    {
        IsSuccess = true;
    }
}
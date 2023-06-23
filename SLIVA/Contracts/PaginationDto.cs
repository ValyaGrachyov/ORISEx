namespace Contracts;

public record PaginationDto(int Page, int PageSize, DateTime StartDate, DateTime EndDate);
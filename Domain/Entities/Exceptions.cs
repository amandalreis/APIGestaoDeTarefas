namespace Domain.Entities;

public class BadRequestException : Exception 
{
    public BadRequestException(string innerException) : base(innerException) {}
}

public class NotFoundException : Exception
{
    public NotFoundException(string innerException) : base(innerException) {}
}

public class ConflictException : Exception
{
    public ConflictException(string innerException) : base(innerException) { }
}
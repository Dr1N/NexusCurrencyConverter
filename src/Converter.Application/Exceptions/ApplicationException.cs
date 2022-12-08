using System.Runtime.Serialization;

namespace Converter.Application.Exceptions;

/// <summary>
/// Application level exception
/// </summary>
[Serializable]
public class ApplicationException : Exception
{
    public ApplicationException()
    {
    }

    protected ApplicationException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }

    public ApplicationException(string message) 
        : base(message)
    {
    }

    public ApplicationException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}

namespace Shoppy.Domain.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public string? Code { get; private set; }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}

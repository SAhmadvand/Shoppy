namespace Shoppy.Shared
{
    public abstract class ExceptionMessage
    {
        protected ExceptionMessage(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; private set; }
        public string Code { get; private set; }
    }
}
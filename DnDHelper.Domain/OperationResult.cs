namespace DnDHelper.Domain
{
    public class OperationResult
    {
        public OperationResultType Result { get; set; }
        public string Message { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult() {Result = OperationResultType.Success};
        }

        public static OperationResult Warning(string message)
        {
            return new OperationResult() {Result = OperationResultType.Warning, Message = message};
        }

        public static OperationResult Error(string message)
        {
            return new OperationResult() {Result = OperationResultType.Error, Message = message};
        }
    }

    public enum OperationResultType { Success, Warning, Error }
}
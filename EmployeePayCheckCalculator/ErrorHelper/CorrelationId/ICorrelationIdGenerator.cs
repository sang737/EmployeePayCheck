namespace Helper.CorrelationId
{
    public interface ICorrelationIdGenerator
    {
        public string GenerateCorrelationId();

        public void SetCorrelationId(string correlationId);
    }
}

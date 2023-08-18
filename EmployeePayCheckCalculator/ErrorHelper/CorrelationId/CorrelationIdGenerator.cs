namespace Helper.CorrelationId
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private string _correlationId = Guid.NewGuid().ToString();

        public string GenerateCorrelationId() => _correlationId;

        public void SetCorrelationId(string correlationId)
        {
            _correlationId = correlationId;
        }
    }
}

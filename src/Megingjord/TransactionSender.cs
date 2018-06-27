using System.Threading.Tasks;

namespace Megingjord
{
    public sealed class TransactionSender : ITransactionSender
    {
        private readonly IVeChainThorBlockchain _blockchain;
        private readonly string _signedTransaction;
        
        
        private string _result;
        private bool _resultHasBeenCalculated;


        internal TransactionSender(
            IVeChainThorBlockchain blockchain,
            string signedTransaction)
        {
            _blockchain = blockchain;
            _signedTransaction = signedTransaction;
        }
        
        public string Result
        {
            get
            {
                if (!_resultHasBeenCalculated)
                {
                    CalculateResultAsync().Wait();
                }
                
                return _result;
            }
        }
        
        public async Task<string> Async()
        {
            if (!_resultHasBeenCalculated)
            {
                await CalculateResultAsync();
            }
                
            return _result;
        }

        private async Task CalculateResultAsync()
        {
            _result = await _blockchain.SendRawTransactionAsync(_signedTransaction);
            
            _resultHasBeenCalculated = true;
        }
    }
}
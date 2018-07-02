using System;

namespace Megingjord.Interfaces
{
    public interface ITransactionSourceWithRequiredParams
    {
        IUnsignedTransaction With(
            Action<Transaction.Params> @params);
    }
}
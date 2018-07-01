using System;

namespace Megingjord.Interfaces
{
    public interface ITransactionWithRequiredParams
    {
        IUnsignedTransaction With(Action<Transaction.NonrequiredParams> @params);
    }
}
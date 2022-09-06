using Braintree;
using Microsoft.Extensions.Configuration;
using Models;

namespace Service;

public class PaymentService
{
    private readonly IBraintreeGateway gateway;

    public PaymentService(IConfiguration config)
    {
        IConfigurationSection paymentConfig = config.GetSection("paymentConfig");
        string MERCHANT_ID = paymentConfig.GetSection("merchantId").Value;
        string PUBLIC_KEY = paymentConfig.GetSection("publicKey").Value;
        string PRIVATE_KEY = paymentConfig.GetSection("privateKey").Value;
        gateway = new BraintreeGateway(Braintree.Environment.SANDBOX, MERCHANT_ID, PUBLIC_KEY, PRIVATE_KEY);

    }
    
    public async Task<string> generateToken() {
        return await gateway.ClientToken.GenerateAsync();
    }

    public async Task<bool> makePayment(TransactionDto transaction) {

        TransactionOptionsRequest options = new TransactionOptionsRequest();
        options.SubmitForSettlement = true;

        TransactionRequest request = new TransactionRequest();
        request.Amount = transaction.amount;
        request.PaymentMethodNonce = transaction.nonce;
        request.Options = options;

        Result<Transaction> result = await gateway.Transaction.SaleAsync(request);
        return result.IsSuccess();
    }
}
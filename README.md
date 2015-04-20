# .NET Yandex.Money API SDK

## Requirements

1. .NET 4.5 or later


## Links

1. Yandex.Money API page: [Ru](http://api.yandex.ru/money/),
[En](http://api.yandex.com/money/)

## Getting started

### Installation

[Nuget package](https://www.nuget.org/packages/Yandex.Money.Api.Sdk/)

### Payments from the Yandex.Money wallet

Using Yandex.Money API requires following steps

1. Obtain token URL and redirect user's browser to Yandex.Money service.
Note: `clientId`, `redirectUri`, `clientSecret` are constants that you get,
when [register](https://sp-money.yandex.ru/myservices/new.xml) app in Yandex.Money API.

    ```csharp
    var p = new AuthorizationRequestParams {
        ClientId = ClientId,
        RedirectUri = RedirectUri,
        Scope = Scopes.Compose(new[] {
            Scopes.AccountInfo, Scopes.OperationHistory, Scopes.OperationDetails, Scopes.PaymentP2P()
        })
    };

    var dmhp = new DefaultMobileHostsProvider();

    // navigate user OAuth2 permission page at our side
    WebBrowser.Navigate(dmhp.AuthorizationdUri, p.PostBytes(), _contentHeader);
    ```

2. After that, user fills Yandex.Money HTML form and user is redirected back to
`REDIRECT_URI?code=CODE`.

3. You should immediately exchange `CODE` with `ACCESS_TOKEN`.

    ```csharp
    var tr = new TokenRequest(defaultHttpPostClient, new JsonSerializer())
    {
        Code = "Temporary token",
        ClientId = "Your Client Id",
        RedirectUri = "Uri to inform about status"
        ClientSecret = "Your Client Secret" // not required
    };

    TokenResult token = await tr.Perform();

    authenticator.Token = token.Token;
    ```

4. Now you can use Yandex.Money API.

    ```csharp
    var p2P = new P2PRequestPaymentParams {
        AmountDue = "Sum to pay", To = "User login"
    };

    var rpr = new RequestPaymentRequest(defaultHttpPostClient, new JsonSerializer()) {
        PaymentParams = p2P.GetParams()
    };

    var requestPaymentResult = await rpr.Perform();

    var ppr = new ProcessPaymentRequest(defaultHttpPostClient, new JsonSerializer()) {
        RequestId = requestPaymentResult.RequestID,
        MoneySource = "..."
    };

    var processPaymentResult = await ppr.Perform();
    ```

### Payments from bank cards without authorization

1. Fetch instantce-id(ussually only once for every client. You can store
result in DB).

    ```csharp
    var instanceIdRequest = new InstanceIdRequest < InstanceIdResult > (defaultHttpPostClient, new JsonSerializer < InstanceIdResult > ()) {
        ClientId = ClientId
    };

    var instanceIdResult = await instanceIdRequest.Perform();
    InstanceId = instanceIdResult.InstanceId;
    ```


2. Make request payment

    ```csharp
    var p2P = new P2PRequestPaymentParams {
        AmountDue = "Sum to pay", To = "User login", Message = "..."
    };

    var requestExternalPaymentRequest = new RequestExternalPaymentRequest < RequestPaymentResult > (
            defaultHttpPostClient, new JsonSerializer < RequestPaymentResult > ()) {
        PaymentParams = p2P.GetParams(),
        InstanceId = InstanceId
    };

    var requestPaymentResult = await requestExternalPaymentRequest.Perform();
    ```

3. Process the request with process-payment. 

    ```csharp
     var processExternalPaymentRequest = new ProcessExternalPaymentRequest < ProcessPaymentResult > (
            defaultHttpPostClient, new JsonSerializer < ProcessPaymentResult > ()) {
        RequestId = requestPaymentResult.RequestID,
        InstanceId = InstanceId,
        ExtAuthSuccessUri = "...",
        ExtAuthFailUri = "...",
     };

     var processPaymentResult = await processExternalPaymentRequest.Perform();
     ```



# .NET Yandex.Money API SDK

## Overview

The library allows you to make payments from bank cards and Yandex.Money wallets using Yandex.Money API.
Yandex.Money API documentation: [English](http://tech.yandex.com/money/), [Russian](http://tech.yandex.ru/money/).

## Requirements

The library requires .NET 4.5 or later.

## Getting started

### Installation

Recommended way to use the library in your own project is install it as [Nuget package](https://www.nuget.org/packages/Yandex.Money.Api.Sdk/).

### App Registration

To be able to use the library you should register your application and get your unique *client id*. To register an application please follow the steps described on [this page](http://tech.yandex.com/money/doc/dg/tasks/register-client.xml) (also available in [Russian](http://tech.yandex.ru/money/doc/dg/tasks/register-client.xml)).

### Samples

[Samples](https://github.com/yandex-money/yandex-money-sdk-net-sample).

### Payments from the Yandex.Money wallet

To make payments from Yandex.Money wallet you have to:

1. Obtain [OAuth2 authorization](https://tech.yandex.com/money/doc/dg/reference/request-access-token-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/request-access-token-docpage)) to make payment from user using web browser.
Note: `clientId`, `redirectUri`, `clientSecret` are constants that you get on app registration.

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
    WebBrowser.Navigate(dmhp.AuthorizationdUri, p.PostBytes(), ...);
    ```

2. The user enters his login and password, reviews the list of requested permissions, and either approves or rejects the authorization request.
The application receives an Authorization Response in the form of an HTTP Redirect with either a temporary authorization code or an error code.

3. On success, application should immediately exchange temporary `authorization_code` to permanent `access_token`:

    ```csharp
    var tr = new TokenRequest<TokenResult>(defaultHttpPostClient, new JsonSerializer<TokenResult>())
    {
        Code = "Temporary token",
        ClientId = "Your Client Id",
        RedirectUri = "Uri to inform about status"
        ClientSecret = "Your Client Secret" // not required
    };

    TokenResult token = await tr.Perform();
    
    authenticator.Token = token.Token;

    ```

4. Next, you able to [make payments](https://tech.yandex.com/money/doc/dg/reference/process-payments-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/process-payments-docpage/)), for example transfers to another user:

    ```csharp
    var p2P = new P2PRequestPaymentParams {
        AmountDue = "Amount to transfer", To = "User login, email or phone"
    };

    var rpr = new RequestPaymentRequest<RequestPaymentResult>(defaultHttpPostClient, new JsonSerializer<RequestPaymentResult>()) {
        PaymentParams = p2P.GetParams()
    };

    var requestPaymentResult = await rpr.Perform();

    if(requestPaymentResult.Status == ResponseStatus.Success) {
        var ppr = new ProcessPaymentRequest<ProcessPaymentResult>(defaultHttpPostClient, new JsonSerializer<ProcessPaymentResult>()) {
            RequestId = requestPaymentResult.RequestID,
            MoneySource = "..."
        };

        var processPaymentResult = await ppr.Perform();
    
        if(processPaymentResult.Status == ResponseStatus.Success) {
            // success
        }
    }

    ```

### Payments from bank cards without authorization

To make [payments from bank cards](https://tech.yandex.com/money/doc/dg/reference/process-external-payments-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/process-external-payments-docpage/)) you have to:

1. [Registering an instance](https://tech.yandex.com/money/doc/dg/reference/instance-id-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/instance-id-docpage/)) of the application. **`instance_id` must be obtained only once and stored within application.**

    ```csharp
    var instanceIdRequest = new InstanceIdRequest < InstanceIdResult > (defaultHttpPostClient, new JsonSerializer < InstanceIdResult > ()) {
        ClientId = ClientId
    };

    var instanceIdResult = await instanceIdRequest.Perform();

    if(instanceIdResult.Status == ResponseStatus.Success) {
        InstanceId = instanceIdResult.InstanceId;
    }

    ```

2. [Create payment](https://tech.yandex.com/money/doc/dg/reference/request-external-payment-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/request-external-payment-docpage/)):

    ```csharp
    var p2P = new P2PRequestPaymentParams {
        AmountDue = "Sum to pay", To = "User login", Message = "..."
    };

    var requestExternalPaymentRequest = new RequestExternalPaymentRequest<RequestPaymentResult> (
            defaultHttpPostClient, new JsonSerializer<RequestPaymentResult> ()) {
        PaymentParams = p2P.GetParams(),
        InstanceId = InstanceId
    };

    var requestPaymentResult = await requestExternalPaymentRequest.Perform();

    if(requestPaymentResult.Status == ResponseStatus.Success) {
        // success
    }
    ```

3. [Confirm payment](https://tech.yandex.com/money/doc/dg/reference/process-external-payment-docpage/)([ru](https://tech.yandex.ru/money/doc/dg/reference/process-external-payment-docpage/)):

    ```csharp
     var processExternalPaymentRequest = new ProcessExternalPaymentRequest<ProcessPaymentResult> (
            defaultHttpPostClient, new JsonSerializer<ProcessPaymentResult>()) {
        RequestId = requestPaymentResult.RequestID,
        InstanceId = InstanceId,
        ExtAuthSuccessUri = "...",
        ExtAuthFailUri = "...",
     };

     var processPaymentResult = await processExternalPaymentRequest.Perform();
     
     if(processPaymentResult.Status == ResponseStatus.Success) {
        // success
     }

     if(processPaymentResult.Status == ResponseStatus.ExtAuthRequired) {

	WebBrowser.Navigate(processPaymentResult.AcsUri, processPaymentResult.AcsParams);
     }

     ```

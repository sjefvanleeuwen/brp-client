# BRP Client - dotnet core

[![nuget](https://img.shields.io/nuget/v/HaalCentraal.BRPBevragen)](https://www.nuget.org/packages/HaalCentraal.BRPBevragen/)
![.NET Core](https://github.com/sjefvanleeuwen/brp-client/workflows/.NET%20Core/badge.svg)

## Wat is het?

Een Haal Centraal BRP Bevragings API client voor dotnet core.

BRP Bevragen is een Haal Centraal API voor het zoeken en raadplegen van ingeschreven natuurlijke personen voor alle binnengemeentelijke afnemers in NL. De informatie die de API levert is herleidbaar naar het LO GBA 3.10.

De API definities kunnen hier
bekeken worden:
https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen

## Hoe werkt het?

### API Client Installeren

```bash
dotnet add package HaalCentraal.BRPBevragen
```

### API Client Configureren

De Client behoeft 2 settings, te weten `ApiKey` en `ApiUrl`. Een API key is aan tevragen bij
https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen

#### appsettings.json


```Json
{
  "Brp": {
    "ApiKey": "your-haal-centraal-api-key",
    "ApiUrl": "https://www.haalcentraal.nl/haalcentraal/api/brp/"
  }
}
```

#### Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddBrpClient(s=> {
        s.ApiKey = Configuration["Brp:ApiKey"];
        s.ApiUrl = Configuration["Brp:ApiUrl"];
    });
}
```

### API Client Gebruiken

Het bijgevoegde voorbeeld (in Blazor) `inject` de API Client.

#### Index.razor

```csharp
@page "/"
@inject IBrpClient brp
<h1>Ingeschreven natuurlijk persoon</h1>

@persoon?.Verblijfplaats?.Straatnaam @persoon?.Verblijfplaats?.Huisnummer
<br />
@persoon?.Verblijfplaats?.Postcode  @persoon?.Verblijfplaats?.Woonplaatsnaam

@code {

    IngeschrevenPersoonHal persoon { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        persoon = await brp.IngeschrevenNatuurlijkPersoonAsync("999993872", null, null);
        StateHasChanged();
    }
}
```

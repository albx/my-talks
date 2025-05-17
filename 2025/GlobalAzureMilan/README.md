# Ricerche intelligenti con Azure AI Search

Questo repository contiene la demo del [talk](https://www.azuremeetupmilano.it/e/sessione/3679/Ricerche-intelligenti-con-Azure-AI-Search) tenuto al [Global Azure Milano 2025](https://www.azuremeetupmilano.it/e/3614/Global-Azure-2025).

## Prerequisiti

- .NET 9
- [Microsoft.Extensions.AI Template](https://learn.microsoft.com/en-us/dotnet/ai/quickstarts/ai-templates?tabs=visual-studio%2Cconfigure-visual-studio&pivots=azure-openai) (attualmente preview).

Nella propria subscription di Azure creare uno Storage Account e un Blob container.

Creare una risorsa di tipo **Azure Open AI** e deployare tre modelli:
- text-embedding-ada002
- text-embedding-3-small
- gpt4o

Provisionare una risorsa di **Azure AI Search**. Per farlo è possibile seguire questo video:

[https://www.youtube.com/watch?v=Ew8UBPB84Bc](https://www.youtube.com/watch?v=Ew8UBPB84Bc).

**NOTA: Attenzione al tier di Azure AI Search per non incorrere in costi indesiderati**.

Una volta creata la risorsa di Azure AI Search configurare due indici per la ricerca vettoriale, uno che utilizzi il modello text-embedding-ada002, l'altro che utilizzi il modello text-embedding-3-small.

Come guida è possibile seguire questo video:

[https://www.youtube.com/watch?v=eufmL2vVCTg](https://www.youtube.com/watch?v=eufmL2vVCTg).

## Progetti

Il progetto *SmartSearches.Web* contiene una applicazione Blazor Interactive Server.

Nel file `appsettings.json` andare ad impostare endpoint e API Key della risorsa Azure AI Search creata.

Nel file `Home.razor.cs` modificare a riga 17 l'elenco degli indici impostando i nomi degli indici creati nella risorsa AI Search.

Il progetto *SmartSearches.ChatApp* contiene una chat che caricherà i documenti presenti nella cartella wwwroot in un indice dedicato nella risorsa Azure AI Search.

Anche qui impostare endpoint e API Key della risorsa di AI Search creata e modificare nel file `Program.cs` i punti necessari con i nomi delle risorse e dei modelli utilizzati.

E' presente un file [README](./SmartSearches.ChatApp/README.md) che contiene le istruzioni necessarie.

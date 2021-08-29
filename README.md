# BlobStore
Api for communicating with azure blob storage

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ul>
    <li><a href="#about-the-project">About The Project</a></li>
    <li><a href="#technologies">Technologies</a></li>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#sdk">SDK</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ul>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This is a api that uploads files to azure blob storage. have ability to create, delete and retrieve blobs and containers. it provides it's own sdk.

<!-- TECHOLOGIES -->
## Technologies
* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
* [Serilog](https://serilog.net/)

<!-- GETTING STARTED -->
## Getting Started

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install [Git](https://git-scm.com/)
3. Clone the repo
 
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
4. Create a folder for your solution and cd into it
5. Navigate to `src/API`
6. you will need to update **appsettings.json** and **appsettings.Development.json** and provide your azure blob storage connection string:
  ```json
    "ConnectionStrings": {
      "AzureBlobStorage": ""
    },
  ```
7. run `dotnet run`


<!-- SDK -->
## SDK

Project provides it's own sdk, which you can publish and use in several projects, without writing intergration code in every project.

inject blob client into dependency injection container
pass published api url and service lifetime(default if Scoped)
```csharp
services.AddBlobClient(x=>x.Url = "");
```

use IBlobClient and IContainerClient interfaces
```csharp

private readonly IContainerClient _containerClient;
private readonly IBlobClient _blobClient;

public ExampleService(IContainerClient containerClient,IBlobClient blobClient)
{
  _containerClient = containerClient;
  _blobClient = blobClient;
}

public async Task Example(CancellationToken cancellationToken)
{
  var containerList = await _containerClient.ListAsync(cancellationToken);
  var blobList = await _blobClient.ListAsync("containername",cancellationToken);
}
```

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

This project is licensed with the [MIT license](LICENSE).

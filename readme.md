# CandidateProductStore

## Projects
| Project | Description |
| - | - |
| Demo.App | The startup application, containing a HostedService using the store. |
| Demo.Abstractions | Abstractions containing the necessary features of the buisness logic layer |
| Demo.Store | The buisness logic layer |
| Demo.StoreApi.Abstractions | Abstractions containing the necessary features of a store API |
| Demo.StoreApi.DeverythingApi | An implementation of a store API using the Deverything API |

## Configuration

| Key | Description |
| - | - |
| ProductStoreApi:BaseUrl | The base URL for a Deverything WebAPI (ending with a '/') |
| ProductStoreApi:User | An API User for the Deverything WebAPI (belongs in UserSecrets) |
| ProductStoreApi:ApiKey | An API Key for the Deverything WebAPI (belongs in UserSecrets) |

## General philosophy

* Error handling is only done if either useful information can be added or if the issue can be resolved.
* Code should be self explanatory (if you need comments it's likely because the code is poorly named or formatted).
* Comments are allowed if they inform why the code works the way it does.


## Future

* Testing could be added in the future, which would be useful before a launch, but will be skipped for now.
* Adding a custom HttpClient in Demo.StoreApi.DeverythingApi would allow for better testing of the serialization and deserialization of requests.

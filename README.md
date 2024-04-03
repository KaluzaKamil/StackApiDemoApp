# StackApiDemoApp

StackApiDemoApp is a dockerized demo api application which gets 1000 tags from [StackOverflow tags api](https://api.stackexchange.com/docs/tags), saves it in a dockerized database created on Sql Express server and then gives you the option to ask for tags with paginated, sorted GetTags action.

## Requirements

Installed [.NET](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net80)

Installed [Docker Engine](https://docs.docker.com/engine/install/)

## Running the app
Copy the repository

Open CMD/PowerShell in the main StackApiDemoApp folder

Run the following command
```bash
docker compose up
```

After about 20-30 seconds you should be able to access the swagger in your web browser under https://localhost:8081/swagger URL

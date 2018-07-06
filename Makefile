all:
	dotnet msbuild

run:
	dotnet run --project "Terminal/Terminal.csproj"

exe:
	dotnet msbuild -r win10-x64
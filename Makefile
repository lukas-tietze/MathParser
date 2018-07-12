all: build

build:
	dotnet msbuild

run: build
	dotnet run --project "Terminal/Terminal.csproj"

exe:
	dotnet msbuild -r win10-x64
	mv Terminal/bin/Debugnetcoreapp2.0/Terminal.exe bin/

# unx:
	# dotnet msbuild -r linuxmint.18.1-x64
	# mv Terminal/bin/Debugnetcoreapp2.0/Terminal bin/
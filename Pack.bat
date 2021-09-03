@echo off
echo BUILD PACKAGES
del .\Pythia.Chiron.Plugin\bin\Debug\*.*nupkg
del .\Pythia.Cli.Plugin.Chiron\bin\Debug\*.*nupkg

cd .\Pythia.Chiron.Plugin
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Pythia.Cli.Plugin.Chiron
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..
pause

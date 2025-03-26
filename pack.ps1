$suffixes = @('', '.Core', '.Core.fil-PH')

foreach ($suffix in $suffixes)
{
	dotnet clean .\BibleReference\BibleReference\BibleReference.csproj --configuration Release
	dotnet pack .\BibleReference\BibleReference\BibleReference.csproj -p:suffix=$($suffix) --configuration Release
}
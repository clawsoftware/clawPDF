msbuild.exe SystemInterface\SystemInterface.csproj /p:Configuration="Release" /p:Platform="AnyCPU" /t:Build
msbuild.exe SystemWrapper\SystemWrapper.csproj /p:Configuration="Release" /p:Platform="AnyCPU" /t:Build

nuget.exe pack SystemInterface\SystemInterface.nuspec -OutputDirectory publish -Properties Configuration="Release" -Version "0.27.0" -Symbols -SymbolPackageFormat snupkg -NonInteractive -ForceEnglishOutput
nuget.exe pack SystemWrapper\SystemWrapper.nuspec -OutputDirectory publish -Properties Configuration="Release" -Version "0.27.0" -Symbols -SymbolPackageFormat snupkg -NonInteractive -ForceEnglishOutput

nuget.exe sign "publish\*.nupkg" -CertificateFingerprint "AC6DBFFB1BF8B62281DEB8641023A66CDDC5DB57" -HashAlgorithm SHA256 -Timestamper http://timestamp.comodoca.com -TimestampHashAlgorithm SHA256 -Overwrite -OutputDirectory publish\signed -NonInteractive -ForceEnglishOutput
nuget.exe sign "publish\*.snupkg" -CertificateFingerprint "AC6DBFFB1BF8B62281DEB8641023A66CDDC5DB57" -HashAlgorithm SHA256 -Timestamper http://timestamp.comodoca.com -TimestampHashAlgorithm SHA256 -Overwrite -OutputDirectory publish\signed -NonInteractive -ForceEnglishOutput

#!/bin/bash

# configure the script variables

package_dir=./package
output_dir=./output
version=$1

rm -rf $package_dir && mkdir $package_dir
rm -rf $output_dir && mkdir $output_dir

# build portable library
msbuild ../../Virgil.CryptoAPI/Virgil.CryptoAPI.csproj /t:Rebuild  /p:Configuration=Release

cp -r ./template.nuspec $package_dir/Virgil.CryptoAPI-$version.nuspec
sed -i '' "s/%version%/$version/g" $package_dir/Virgil.CryptoAPI-$version.nuspec

nuget pack $package_dir/Virgil.CryptoAPI-$version.nuspec  -OutputDirectory $output_dir

#rm -rf $package_dir




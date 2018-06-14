#!/bin/bash

# configure the script variables

working_dir=./working
package_dir=./package
output_dir=./output
crypto_core_dir=$working_dir/crypto_core
net_framework='net_fr'
mono_framework='mono_fr'
win_platform='win'
osx_platform='osx'
netstandard_framework='netstandard1.1'
cdn_base_url="https://cdn.virgilsecurity.com/virgil-crypto/net/virgil-crypto"
version=$1
liba1=( "$cdn_base_url-$version-mono-android-21.tgz"         'android' 'monoandroid'               )
liba2=( "$cdn_base_url-$version-mono-ios-9.0.tgz"            'ios'     'xamarinios'                )
liba3=( "$cdn_base_url-$version-mono-linux-x86_64.tgz"       'linux'   $mono_framework                          )
liba4=( "$cdn_base_url-$version-net-windows-6.3.zip"         $win_platform     $net_framework              )
liba5=( "$cdn_base_url-$version-mono-darwin-17.5-x86_64.tgz" $osx_platform     $netstandard_framework        )
liba6=( "$cdn_base_url-$version-mono-darwin-17.5-x86_64.tgz" $osx_platform     'xamarinmac20'        )
# create or clear a working and package dirs

rm -rf $working_dir && mkdir $working_dir && mkdir $crypto_core_dir
rm -rf $package_dir && mkdir $package_dir
rm -rf $output_dir && mkdir $output_dir


# download the crypto libs form the virgil's CDN

cd $working_dir

urls=()
for i in ${!liba@}; do
    eval url=( \${$i[@]} )
    urls+=( "-O ${url[0]}" )
done

curl --fail --show-error ${urls[@]}

cd ..

if [[ $? != 0 ]] ; then
    echo "An error has occurred in files downloading"
    exit 1
fi


# build wrapper Virgil.CryptoImpl  

msbuild ../../Sources/Virgil.CryptoImpl/Virgil.CryptoImpl.csproj /t:Rebuild  /p:Configuration=Release
cp ../../Sources/Virgil.CryptoImpl/bin/Release/netstandard1.1/Virgil.CryptoImpl.dll $crypto_core_dir

# run loop through all libs in the libs

for i in ${!lib@}; do
    eval lib=( \${$i[@]} )

    platform=${lib[1]}  
    
    # example: virgil-crypto-2.3.1-net-windows-6.3.zip
    archive_name="$(basename ${lib[0]})"
    file_name="${archive_name%.*}"

    # create a platform dir
    mkdir -p $working_dir/$platform
    
    # extract archive into a platform dir
    tar -xvzf $working_dir/$archive_name -C $working_dir/$platform

    # create a platform for 
    mkdir -p $package_dir/runtimes/$platform/native

    rsync -aP --exclude=Virgil.Crypto.dll $working_dir/$platform/$file_name/lib/* $package_dir/runtimes/$platform/native
    
    IFS=',' read -ra frameworks <<< "${lib[2]}"
    for framework in "${frameworks[@]}"; do

        # For OSX, Linux and Windows we keep relevant Virgil.Crypto.dll in runtime library. We will reference it according to OS in runtime.
        if [[ ($framework == $net_framework) || ($framework == $netstandard_framework) || ($framework == $mono_framework)]]; then
            mkdir $package_dir/runtimes/$platform/lib/
            cp -r $working_dir/$platform/$file_name/lib/Virgil.Crypto.dll $package_dir/runtimes/$platform/lib/  
        fi    

        
        # net_framework and mono_framework doesn't require separate crypto wrapper Virgil.CryptoImpl.dll and msbuild targets - will load from netstandard
        if [[ ($framework != $net_framework) && ($framework != $mono_framework) ]]; then
            # add crypto wrapper to each framework
            mkdir -p $package_dir/lib/$framework
            cp $crypto_core_dir/Virgil.CryptoImpl.dll $package_dir/lib/$framework

            # xamarin.mac, xamarinios and monoandroid will load Virgil.Crypto.dll from own folder under lib
            # Attention! lib/netstandard1.1 has OSX Virgil.Crypto.dll because we need to get Virgil.Crypto.dll referenced 
            # immediately after package installation. But unfortunatly nuget doesn't allow to have condition in <reference> tag 
            # in nuspec file. So for Windows we reference correct Virgil.Crypto.dll in MSBuild netstandard.targets.
            cp -r $working_dir/$platform/$file_name/lib/Virgil.Crypto.dll $package_dir/lib/$framework/

            mkdir -p $package_dir/build/$framework
            cp -r ./targets/$framework.targets $package_dir/build/$framework/Virgil.Crypto.targets    
        fi    

    done
done

cp -r ./template.nuspec $package_dir/Virgil.Crypto-$version.nuspec
sed -i '' "s/%version%/$version/g" $package_dir/Virgil.Crypto-$version.nuspec

nuget pack $package_dir/Virgil.Crypto-$version.nuspec  -OutputDirectory $output_dir

rm -rf $working_dir
#rm -rf $package_dir




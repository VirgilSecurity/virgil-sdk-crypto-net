#!/bin/bash

# configure the script variables

working_dir=./working
package_dir=./package
output_dir=./output
crypto_core_dir=$working_dir/crypto_core
net_framework='net45'
cdn_base_url="https://cdn.virgilsecurity.com/virgil-crypto/net/virgil-crypto"
version=$1
liba1=( "$cdn_base_url-$version-mono-android-21.tgz"         'android' 'monoandroid'               )
liba2=( "$cdn_base_url-$version-mono-ios-9.0.tgz"            'ios'     'xamarinios'                )
liba3=( "$cdn_base_url-$version-mono-linux-x86_64.tgz"       'linux'   ''                          )
liba4=( "$cdn_base_url-$version-net-windows-6.3.zip"         'win'     $net_framework              )
liba5=( "$cdn_base_url-$version-mono-darwin-17.5-x86_64.tgz" 'osx'     'netstandard1.1'        )

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

msbuild ../Virgil.CryptoImpl/Virgil.CryptoImpl.csproj /t:Rebuild  /p:Configuration=Release
cp ../Virgil.CryptoImpl/bin/Release/netstandard1.1/Virgil.CryptoImpl.dll $crypto_core_dir

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
        mkdir -p $package_dir/lib/$framework
        cp -r $working_dir/$platform/$file_name/lib/Virgil.Crypto.dll $package_dir/lib/$framework/
        
        # add crypto wrapper to each framework
        cp $crypto_core_dir/Virgil.CryptoImpl.dll $package_dir/lib/$framework

        # net_framework doesn't require separate msbuild targets - will load from netstandard
        if [ $framework != $net_framework ]; then
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




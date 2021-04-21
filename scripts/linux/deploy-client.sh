#! /bin/bash

cd ~/BootstrapAdmin
git pull
dotnet publish Bootstrap.Client -c Release

rm -f ~/Bootstrap.Client/bin/Release/netcoreapp3.1/publish/appsettings*.json
systemctl stop ba.client
\cp -fr ~/Bootstrap.Client/bin/Release/netcoreapp3.1/publish/* /usr/local/ba/client/
systemctl start ba.client
systemctl status ba.client -l

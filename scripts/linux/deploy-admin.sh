#! /bin/bash

cd ~/BootstrapAdmin
git pull
dotnet publish Bootstrap.Admin -c Release

rm -f ~/Bootstrap.Admin/bin/Release/netcoreapp3.1/publish/appsettings*.json
rm -f ~/Bootstrap.Admin/bin/Release/netcoreapp3.1/publish/BootstrapAdmin.db

systemctl stop ba.admin
\cp -fr ~/Bootstrap.Admin/bin/Release/netcoreapp3.1/publish/* /usr/local/ba/admin/
systemctl start ba.admin
systemctl status ba.admin -l

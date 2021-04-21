#! /bin/bash

mkdir /usr/local/ba
mkdir /usr/local/ba/admin
mkdir /usr/local/ba/client

cp ~/Bootstrap.Admin/appsettings.json /usr/local/ba/admin
cp ~/Bootstrap.Admin/BootstrapAdmin.db /usr/local/ba/admin
cp ~/Bootstrap.Client/appsettings.json /usr/local/ba/client

cp ~/scripts/linux/services/* /usr/lib/systemd/system
systemctl enable ba.admin
systemctl enable ba.client
systemctl enable nginx

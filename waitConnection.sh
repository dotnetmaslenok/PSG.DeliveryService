#!/bin/sh
pg_isready -h ds_postgres -p 5432 -U ds_postgres
if [ $state -ne "0" ]; then
    echo "Waiting for postgres";
else
    echo $state;
    echo "Succeeded";
    sleep 5;
fi
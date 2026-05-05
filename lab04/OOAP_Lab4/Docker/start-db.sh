#!/bin/bash

CONTAINER_NAME="cert-lab-postgres"
DB_NAME="certdb"
DB_USER="certuser"
DB_PASSWORD="certpass"
DB_PORT="5433"

echo "=== ЗАПУСК POSTGRESQL ==="

docker stop $CONTAINER_NAME 2>/dev/null
docker rm $CONTAINER_NAME 2>/dev/null

docker run -d \
  --name $CONTAINER_NAME \
  -e POSTGRES_DB=$DB_NAME \
  -e POSTGRES_USER=$DB_USER \
  -e POSTGRES_PASSWORD=$DB_PASSWORD \
  -p $DB_PORT:5432 \
  postgres:16

echo ">>> Ожидание готовности..."
until docker exec $CONTAINER_NAME pg_isready -U $DB_USER -d $DB_NAME > /dev/null 2>&1; do
  sleep 1
done

echo "=== ГОТОВО, ЗАПУСКАЙ init.sql ==="
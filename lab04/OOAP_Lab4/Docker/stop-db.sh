#!/bin/bash

CONTAINER_NAME="cert-lab-postgres"

echo "=========================================="
echo "  ОСТАНОВКА POSTGRESQL"
echo "=========================================="

echo "[1/2] Остановка контейнера..."
docker stop $CONTAINER_NAME 2>/dev/null

echo "[2/2] Удаление контейнера..."
docker rm $CONTAINER_NAME 2>/dev/null

echo ""
echo "Готово! База данных остановлена."
echo ""
echo "Для повторного запуска: ./docker/start-db.sh"
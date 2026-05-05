Запуск необходимо осуществлять из терминала

**Запуск базы данных**
cd /home/nobahu/IdeaProjects/OOAP_Lab4
./Docker/start-db.sh

**Инициализация таблицы сертификатов**
docker exec -i cert-lab-postgres psql -U certuser -d certdb < Docker/init.sql

**Остановка базы данных**
cd /home/nobahu/IdeaProjects/OOAP_Lab4
./Docker/stop-db.sh
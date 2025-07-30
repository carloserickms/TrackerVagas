version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: App/Dockerfile
    ports:
      - "8080:80"
    env_file:
      - .env
    depends_on:
      db:
        condition: service_healthy # Garante que o DB esteja totalmente pronto antes da API iniciar
    working_dir: /app
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      # A string de conexão para o MySQL local será injetada via .env
      # Certifique-se de que seu .env tenha: DATABASECONNECTION=Server=db;Port=3306;Database=TrackerVagasdb;UserId=root;Password=semsenha
    healthcheck: # Healthcheck para a própria API, verificando se ela está respondendo
      test: ["CMD", "curl", "--fail", "http://localhost:80/health"]
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 20s # Dá um tempo para a aplicação subir e executar migrações/seeds

  db:
    image: mysql:8.0
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD}
      MYSQL_DATABASE: ${MYSQL_DATABASE}
    ports:
      - "3307:3306" # Mapeia a porta 3306 do contêiner para a 3307 no seu host
    volumes:
      - mysql-data:/var/lib/mysql # Volume persistente para os dados do MySQL
    healthcheck: # Healthcheck robusto para o MySQL, verificando conexão real
      test: ["CMD", "sh", "-c", "mysql -u root -p${MYSQL_ROOT_PASSWORD} -h 127.0.0.1 -P 3306 -e 'SELECT 1;'"]
      interval: 5s
      timeout: 20s
      retries: 15
      start_period: 60s # Dá um tempo extra para a inicialização complexa do MySQL

volumes:
  mysql-data: # Definição do volume para os dados do MySQL
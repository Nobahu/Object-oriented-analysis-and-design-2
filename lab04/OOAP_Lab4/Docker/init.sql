-- Подключение: psql -U certuser -d certdb

-- Таблица для CA сертификатов
CREATE TABLE IF NOT EXISTS ca_certificates (
    id SERIAL PRIMARY KEY,
    subject_name VARCHAR(500) NOT NULL,
    not_before TIMESTAMP NOT NULL,
    not_after TIMESTAMP NOT NULL,
    algorithm VARCHAR(100) NOT NULL,
    public_key_info VARCHAR(500) NOT NULL,
    certificate_bytes BYTEA NOT NULL
    );

-- Таблица для пользовательских сертификатов
CREATE TABLE IF NOT EXISTS user_certificates (
    id SERIAL PRIMARY KEY,
    subject_name VARCHAR(500) NOT NULL,
    issuer_name VARCHAR(500) NOT NULL,
    not_before TIMESTAMP NOT NULL,
    not_after TIMESTAMP NOT NULL,
    algorithm VARCHAR(100) NOT NULL,
    public_key_info VARCHAR(500) NOT NULL,
    certificate_bytes BYTEA NOT NULL
    );

CREATE TABLE IF NOT EXISTS ca_key (
    id SERIAL PRIMARY KEY,
    ca_id INTEGER REFERENCES ca_certificates(id),
    private_key_info BYTEA NOT NULL
)
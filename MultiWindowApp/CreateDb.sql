CREATE TYPE gender_enum AS ENUM ('Unspecified', 'Male', 'Female');

CREATE TABLE table_users (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    gender gender_enum NOT NULL DEFAULT 'Unspecified',
    birth_date DATE NOT NULL,
    email TEXT NOT NULL UNIQUE,
    password TEXT NOT NULL,
    is_admin BOOLEAN NOT NULL DEFAULT FALSE,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
)